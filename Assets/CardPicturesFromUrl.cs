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

            Debug.Log("Loading started");

            foreach (var cardType in cardTypes)
            {
                var metadata = cardType.Metadata;
                var pictureUrl = metadata.PictureUrl;

                if (!string.IsNullOrEmpty(pictureUrl))
                {
                    tasks.Add(GetSpriteAsync(pictureUrl));
                }
            }

            await UniTask.WhenAll(tasks).ContinueWith(() => { Debug.Log("Loading finished"); });
        }

        async UniTask GetSpriteAsync(string pictureUrl)
        {
            // Debug.Log("start loading...");
            var req = UnityWebRequestTexture.GetTexture(pictureUrl);
            var op = await req.SendWebRequest();
            var tex = ((DownloadHandlerTexture)(op.downloadHandler)).texture;
            // Debug.Log("finished loading...");

            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            SpritesByUrl.Add(pictureUrl, sprite);

            if (Subscriptions.TryGetValue(pictureUrl, out var subsriptionsForUrl))
            {
                foreach (var sub in subsriptionsForUrl)
                {
                    sub?.Invoke(sprite);
                }

                Subscriptions[pictureUrl] = new List<Action<Sprite>>();
            }
        }
    }
}
