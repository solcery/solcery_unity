using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Solcery
{
    public class CardPicturesFromUrl : Singleton<CardPicturesFromUrl>
    {
        public Dictionary<string, Sprite> SpritesByUrl;
        public Dictionary<string, List<Action<Sprite>>> Subscriptions = new Dictionary<string, List<Action<Sprite>>>();

        public void GetTextureByUrl(string url, Action<Sprite> onSpriteReady)
        {
            if (SpritesByUrl.TryGetValue(url, out var sprite))
                onSpriteReady?.Invoke(sprite);
            else
            {
                if (Subscriptions.TryGetValue(url, out var subsriptionsForUrl))
                    subsriptionsForUrl.Add(onSpriteReady);
                else
                    Subscriptions.Add(url, new List<Action<Sprite>>() { onSpriteReady });
            }
        }

        public async UniTask BasicLoad(GameContent gameContent)
        {
            if (gameContent == null || gameContent.CardTypes == null)
                return;

            SpritesByUrl = new Dictionary<string, Sprite>();
            var cardTypes = gameContent.CardTypes;

            var tasks = new List<UniTask>();

            foreach (var cardType in cardTypes)
            {
                var metadata = cardType.Metadata;
                var url = metadata.PictureUrl;

                if (!string.IsNullOrEmpty(url))
                {
                    tasks.Add(GetSpriteAsync(url));
                }
            }

            await UniTask.WhenAll(tasks);
        }

        async UniTask GetSpriteAsync(string url)
        {
            // Debug.Log("start loading...");
            var req = UnityWebRequestTexture.GetTexture(url);
            var op = await req.SendWebRequest();
            var www = (DownloadHandlerTexture)(op.downloadHandler);

            Texture2D wwwTex = DownloadHandlerTexture.GetContent(req);
            Texture2D newTex = new Texture2D(wwwTex.width, wwwTex.height);
            newTex.SetPixels(wwwTex.GetPixels(0));
            newTex.Apply(true); // default is to update mipmaps

            // var tex = www.texture;
            // tex.SetPixels(tex.GetPixels(0, 0, tex.width, tex.height));
            // Debug.Log("finished loading...");
            // Debug.Log(Time.realtimeSinceStartup);

            // tex.filterMode = FilterMode.Point;

            // tex.SetPixels(tex.GetPixels(1));
            // tex.Apply(true);

            var sprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 100.0f);
            SpritesByUrl.Add(url, sprite);

            NotifySubscribers(url, sprite);
        }

        private void NotifySubscribers(string url, Sprite sprite)
        {
            if (Subscriptions.TryGetValue(url, out var subsriptionsForUrl))
            {
                foreach (var sub in subsriptionsForUrl)
                {
                    sub?.Invoke(sprite);
                }

                Subscriptions[url] = new List<Action<Sprite>>();
            }
        }
    }
}
