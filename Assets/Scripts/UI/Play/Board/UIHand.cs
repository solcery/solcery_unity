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

        public void Clear()
        {
            DeleteAllCards();
        }

        protected void UpdateWithDiff(CardPlaceDiff cardPlaceDiff, bool areCardsInteractable, bool areCardsFaceDown, bool showCoins, bool areCardsScattered = false)
        {
            _areCardsFaceDown = areCardsFaceDown;

            if (_cardsById == null)
                _cardsById = new Dictionary<int, UIBoardCard>();
            else
            {
                foreach (var idCard in _cardsById)
                {
                    idCard.Value.SetInteractabe(areCardsInteractable);
                }
            }

            if (cardPlaceDiff == null)
                return;

            if (cardPlaceDiff.Stayed != null)
            {
                foreach (var stayedCard in cardPlaceDiff.Stayed)
                {
                    if (!_cardsById.ContainsKey(stayedCard.CardData.CardId))
                    {
                        var card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                        card.Init(stayedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);
                        _cardsById.Add(stayedCard.CardData.CardId, card);
                    }
                    else
                    {
                        if (_cardsById.TryGetValue(stayedCard.CardData.CardId, out var existringCard))
                            existringCard.StopShaking();
                    }
                }
            }

            if (cardPlaceDiff.Departed != null)
            {
                foreach (var departedCard in cardPlaceDiff.Departed)
                {
                    var cardToDelete = GetCardById(departedCard.CardData.CardId);
                    UICardAnimator.Instance?.Clone(cardToDelete, departedCard);
                    DeleteCard(cardToDelete);
                }
            }

            if (cardPlaceDiff.Arrived != null)
            {
                foreach (var arrivedCard in cardPlaceDiff.Arrived)
                {
                    UIBoardCard card;

                    card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                    card.Init(arrivedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);

                    if (arrivedCard.From == CardPlace.Nowhere || !UIBoard.Instance.GetBoardPlace(arrivedCard.From, out var fromPlace))
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
            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        }

        public void OnCardArrival(int cardId)
        {
            var card = GetCardById(cardId);
            if (card != null) card.SetVisibility(true);
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
                return this.transform.position;
            }
        }

        public Vector3 GetCardRotation(int cardId)
        {
            var card = GetCardById(cardId);

            if (card != null)
                return card.transform.localRotation.eulerAngles;
            else
            {
                return this.transform.localRotation.eulerAngles;
            }
        }

        public Transform GetCardsParent()
        {
            return content;
        }
    }
}
