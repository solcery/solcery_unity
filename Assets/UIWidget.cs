using System.Collections.Generic;
using Solcery.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIWidget : MonoBehaviour, IBoardPlace
    {
        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image image = null;
        [SerializeField] private UIDiff diff = null;
        [SerializeField] private TextMeshProUGUI numberText = null;

        private PlaceDisplayData _displayData;
        protected bool _areCardsFaceDown;

        private List<CardData> _cards;
        private CardData _topCardData;
        private CardType _topCardType;
        private int _currentNumber;
        private bool _isInitialNumber = true;

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
            UpdateWithCards(gameContent, _cards);
        }

        public void UpdateWithCards(GameContent gameContent, List<CardData> cards)
        {
            // Debug.Log("UIWidget.UpdateWithCards");

            _cards = cards;

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
                if (image != null && image.gameObject != null)
                    image.gameObject.SetActive(false);
                return;
            }

            if (image != null && image.gameObject != null)
                image?.gameObject?.SetActive(true);

            var pictureUrl = _topCardType.Metadata.PictureUrl;
            var picture = _topCardType.Metadata.Picture;

            if (!string.IsNullOrEmpty(pictureUrl))
            {
                CardPicturesFromUrl.Instance?.GetTextureByUrl(pictureUrl, (sprite) => SetSprite(sprite));
            }
            else if (cardPictures != null)
                SetSprite(cardPictures?.GetSpriteByIndex(picture));

            if (!_topCardData.TryGetAttrValue("number", out var number))
            {
                Debug.Log("no number attribute");
            }
            else
            {
                SetNumber(number);
            }
        }

        private void SetSprite(Sprite sprite)
        {
            if (image == null)
                return;

            image.sprite = sprite;
        }

        private void SetNumber(int newNumber)
        {
            // Debug.Log("UIWidget.SetNumber");

            if (_currentNumber != newNumber)
                if (!_isInitialNumber)
                    diff?.Show(newNumber - _currentNumber);

            _currentNumber = newNumber;
            _isInitialNumber = false;

            if (numberText != null)
                numberText.text = newNumber.ToString();
        }
    }
}
