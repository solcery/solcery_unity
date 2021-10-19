using System.Collections.Generic;
using Solcery.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIPicture : MonoBehaviour, IBoardPlace
    {
        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image image = null;
        [SerializeField] private AspectRatioFitter arf = null;
        [SerializeField] private RectTransform imageRect = null;

        private PlaceDisplayData _displayData;
        protected bool _areCardsFaceDown;

        private bool _stretch;
        private List<CardData> _cards;
        private CardData _topCardData;
        private CardType _topCardType;

        public Vector3 GetCardDestination(int cardId)
        {
            return Vector3.one;
        }

        public Vector3 GetCardRotation(int cardId)
        {
            return Vector3.zero;
        }

        public Vector2 GetCardSize(int cardId)
        {
            return Vector2.one;
        }

        public Transform GetCardsParent()
        {
            return this.transform;
        }

        public void OnCardArrival(int cardId)
        {

        }

        public void UpdateGameContent(GameContent gameContent)
        {
            UpdateWithCards(gameContent, _cards, _stretch);
        }

        public void UpdateWithCards(GameContent gameContent, List<CardData> cards, bool stretch = false)
        {
            _cards = cards;
            _stretch = stretch;

            if (_cards == null || _cards.Count <= 0)
            {
                return;
            }

            _topCardData = _cards[0];

            if (_topCardData == null)
            {
                return;
            }

            _topCardType = gameContent.GetCardTypeById(_topCardData.CardType);

            if (_topCardType == null)
            {
                image?.gameObject?.SetActive(false);
                return;
            }

            image?.gameObject?.SetActive(true);

            var pictureUrl = _topCardType.Metadata.PictureUrl;
            var picture = _topCardType.Metadata.Picture;

            if (!string.IsNullOrEmpty(pictureUrl))
            {
                CardPicturesFromUrl.Instance?.GetTextureByUrl(pictureUrl, (sprite) => SetSprite(sprite));
            }
            else if (cardPictures != null)
                SetSprite(cardPictures?.GetSpriteByIndex(picture), stretch);
        }

        private void SetSprite(Sprite sprite, bool stretch = false)
        {
            if (image == null)
                return;

            if (arf != null)
                arf.enabled = !stretch;

            if (stretch)
            {
                imageRect.offsetMin = Vector2.zero;
                imageRect.offsetMax = Vector2.zero;
            }

            image.preserveAspect = !stretch;
            image.sprite = sprite;
        }
    }
}
