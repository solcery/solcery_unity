using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public abstract class UIHand : MonoBehaviour, IBoardPlace
    {
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] protected GameObject cardPrefab = null;
        [SerializeField] protected Transform content = null;

        protected bool _areCardsFaceDown;

        protected Dictionary<int, UIBoardCard> _cardsById;

        protected void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool areButtonsInteractable, bool areCardsFaceDown, bool showCoins)
        {
            _areCardsFaceDown = areCardsFaceDown;

            if (cardPlaceDiv == null)
                return;

            if (_cardsById == null)
                _cardsById = new Dictionary<int, UIBoardCard>();

            if (cardPlaceDiv.Departed != null)
            {
                foreach (var departedCard in cardPlaceDiv.Departed)
                {
                    var cardToDelete = GetCardById(departedCard.CardData.CardId);
                    UICardAnimator.Instance?.Clone(cardToDelete, departedCard);
                    DeleteCard(cardToDelete);
                }
            }

            if (cardPlaceDiv.Arrived != null)
            {
                foreach (var arrivedCard in cardPlaceDiv.Arrived)
                {
                    UIBoardCard card;

                    card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                    card.Init(arrivedCard.CardData, _areCardsFaceDown, areButtonsInteractable, showCoins, OnCardCasted);

                    if (arrivedCard.From == CardPlace.Nowhere)
                        card.SetVisibility(true);
                    else
                        card.SetVisibility(false);

                    if (_cardsById.ContainsKey(arrivedCard.CardData.CardId))
                        _cardsById[arrivedCard.CardData.CardId] = card;
                    else
                        _cardsById.Add(arrivedCard.CardData.CardId, card);
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        }

        public void OnCardArrival(int cardId)
        {
            GetCardById(cardId).SetVisibility(true);
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

        private UIBoardCard GetCardById(int cardId)
        {
            if (_cardsById.TryGetValue(cardId, out var card))
                return card;

            return null;
        }

        private void DeleteCard(UIBoardCard card)
        {
            if (card != null)
            {
                card?.DeInit();
                DestroyImmediate(card.gameObject);
            }
        }

        public Vector3 GetCardDestination(int cardId)
        {
            var card = GetCardById(cardId);

            if (card != null)
                return card.transform.position;
            else
            {
                Debug.Log("destination card is null");
                return this.transform.position;
            }
        }

        public Transform GetCardsParent()
        {
            return content;
        }
    }
}
