using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UITitle : MonoBehaviour, IBoardPlace
    {
        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] private TextMeshProUGUI titleText = null;
        [SerializeField] protected Image bgImage = null;

        private PlaceDisplayData _displayData;
        protected bool _areCardsFaceDown;

        private List<CardData> _cards;
        private CardData _topCardData;
        private CardType _topCardType;

        public Vector3 GetCardDestination(int cardId)
        {
            return Vector3.one;
        }

        public Vector3 GetCardRotation(int cardId)
        {
            return Vector3.one;
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
            UpdateWithCards(_displayData, gameContent, _cards);
        }

        public void UpdateWithCards(PlaceDisplayData displayData, GameContent gameContent, List<CardData> cards)
        {
            _displayData = displayData;
            _cards = cards;

            SetBgColor();

            if (_cards == null || _cards.Count <= 0)
            {
                if (titleText != null)
                    titleText.text = "No cards in this place";

                return;
            }

            _topCardData = _cards[0];

            if (_topCardData == null)
            {
                if (titleText != null)
                    titleText.text = "Top card is null";

                return;
            }

            _topCardType = gameContent.GetCardTypeById(_topCardData.CardType);

            if (_topCardType == null)
            {
                if (titleText != null)
                    titleText.text = "CardType is null";

                return;
            }

            if (titleText != null)
                titleText.text = _topCardType.Metadata.Description;
        }

        private void SetBgColor()
        {
            if (_displayData.HasBg)
                if (ColorUtility.TryParseHtmlString(_displayData.BgColor, out var bgColor))
                    if (bgImage != null)
                    {
                        bgImage.gameObject.SetActive(true);
                        bgImage.color = bgColor;
                        return;
                    }

            if (bgImage != null)
                bgImage.gameObject.SetActive(false);
        }
    }
}
