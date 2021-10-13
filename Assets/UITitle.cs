using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Solcery.UI
{
    public class UITitle : MonoBehaviour, IBoardPlace
    {
        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] private TextMeshProUGUI titleText = null;

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
            UpdateWithCards(gameContent, _cards);
        }

        public void UpdateWithCards(GameContent gameContent, List<CardData> cards)
        {
            _cards = cards;

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
    }
}
