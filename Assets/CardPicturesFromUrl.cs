using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Solcery.Modules
{
    public class CardPicturesFromUrl : Singleton<CardPicturesFromUrl>
    {
        [HideInInspector] public Dictionary<string, Sprite> SpritesByUrl;
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
                    // tasks.Add(GetSpriteAsync(url));
                    await GetSpriteAsync(url);
                    // Debug.Log($"add task: {tasks.Count}");
                }
            }

            // await UniTask.WhenAll(tasks);
            // Debug.Log("WhenAll");
        }

        Texture2D newTex;

        async UniTask GetSpriteAsync(string url)
        {
            // Debug.Log("start loading...");
            var req = UnityWebRequestTexture.GetTexture(url, true);
            var op = await req.SendWebRequest();

            if (op.result != UnityWebRequest.Result.Success)
                return;
                
            var www = (DownloadHandlerTexture)(op.downloadHandler);

            // Texture2D wwwTex = DownloadHandlerTexture.GetContent(req);
            // Texture2D newTex = new Texture2D(wwwTex.width, wwwTex.height);
            // newTex.SetPixels(wwwTex.GetPixels(0));
            // newTex.Apply(true);

            newTex = www.texture;

            var sprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 100.0f);

            if (!SpritesByUrl.ContainsKey(url))
                SpritesByUrl.Add(url, sprite);
            else
                SpritesByUrl[url] = sprite;

            // Debug.Log("loaded");
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
