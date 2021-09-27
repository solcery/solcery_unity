using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Solcery
{
    public class CardPicturesFromUrl : Singleton<CardPicturesFromUrl>
    {
        public Dictionary<string, Texture> TexturesByUrl;

        private Texture _texture;

        public Texture GetTextureByUrl(string url)
        {
            if (TexturesByUrl.TryGetValue(url, out _texture))
                return _texture;

            return null;
        }

        public async UniTask BasicLoad(GameContent gameContent)
        {
            if (gameContent == null || gameContent.CardTypes == null)
                return;

            Debug.Log("Loading started");

            TexturesByUrl = new Dictionary<string, Texture>();
            var cardTypes = gameContent.CardTypes;

            foreach (var cardType in cardTypes)
            {
                var metadata = cardType.Metadata;
                var pictureUrl = metadata.PictureUrl;

                if (!string.IsNullOrEmpty(pictureUrl))
                {
                    Debug.Log("start...");
                    var texture = ((DownloadHandlerTexture)(await UnityWebRequestTexture.GetTexture(pictureUrl).SendWebRequest()).downloadHandler).texture;
                    TexturesByUrl.Add(pictureUrl, texture);
                    Debug.Log("finish...");
                }
            }

            Debug.Log("Loading finished");
        }
    }
}
