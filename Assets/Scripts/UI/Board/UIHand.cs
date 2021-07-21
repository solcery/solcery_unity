using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Play
{
    public abstract class UIHand : MonoBehaviour, IBoardPlace
    {
        [SerializeField] protected GameObject cardPrefab = null;
        [SerializeField] protected GameObject cardFaceDownPrefab = null;
        [SerializeField] protected Transform content = null;

        protected Dictionary<int, UIBoardCard> _cardsById;

        protected void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool areButtonsInteractable, bool areCardsFaceDown, bool showCoins)
        {
            if (cardPlaceDiv == null)
                return;

            if (_cardsById == null)
                _cardsById = new Dictionary<int, UIBoardCard>();

            if (cardPlaceDiv.Departed != null)
            {
                foreach (var departedCard in cardPlaceDiv.Departed)
                {
                    DeleteCardWithId(departedCard.CardData.CardId);
                }
            }

            if (cardPlaceDiv.Arrived != null)
            {
                Debug.Log(cardPlaceDiv.Arrived.Count);

                foreach (var arrivedCard in cardPlaceDiv.Arrived)
                {
                    UIBoardCard card;

                    if (!areCardsFaceDown)
                    {
                        card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                        card.Init(arrivedCard.CardData, areButtonsInteractable, showCoins, OnCardCasted);
                    }
                    else
                    {
                        card = Instantiate(cardFaceDownPrefab, content).GetComponent<UIBoardCard>();
                    }

                    if (_cardsById.ContainsKey(arrivedCard.CardData.CardId))
                        _cardsById[arrivedCard.CardData.CardId] = card;
                    else
                        _cardsById.Add(arrivedCard.CardData.CardId, card);
                }
            }
        }

        protected abstract void OnCardCasted(int cardId);

        public void DeleteAllCards()
        {
            if (_cardsById != null && _cardsById.Count > 0)
            {
                foreach (var cardById in _cardsById)
                {
                    DeleteCard(cardById.Value);
                }
            }

            _cardsById = new Dictionary<int, UIBoardCard>();
        }

        private void DeleteCardWithId(int cardId)
        {
            if (_cardsById.TryGetValue(cardId, out var card))
                DeleteCard(card);
        }

        private void DeleteCard(UIBoardCard card)
        {
            if (card != null)
            {
                card?.DeInit();
                DestroyImmediate(card.gameObject);
            }
        }
    }
}
