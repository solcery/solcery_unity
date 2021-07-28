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

        protected void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool areCardsInteractable, bool areCardsFaceDown, bool showCoins, bool areCardsScattered = false)
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

            if (cardPlaceDiv == null)
                return;

            if (cardPlaceDiv.Stayed != null)
            {
                foreach (var stayedCard in cardPlaceDiv.Stayed)
                {
                    if (!_cardsById.ContainsKey(stayedCard.CardData.CardId))
                    {
                        var card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                        card.Init(stayedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);
                        _cardsById.Add(stayedCard.CardData.CardId, card);
                    }
                }
            }

            // Debug.Log("hand 1");

            if (cardPlaceDiv.Departed != null)
            {
                foreach (var departedCard in cardPlaceDiv.Departed)
                {
                    var cardToDelete = GetCardById(departedCard.CardData.CardId);
                    UICardAnimator.Instance?.Clone(cardToDelete, departedCard);
                    DeleteCard(cardToDelete);
                }
            }

            // Debug.Log("hand 2");

            if (cardPlaceDiv.Arrived != null)
            {
                foreach (var arrivedCard in cardPlaceDiv.Arrived)
                {
                    UIBoardCard card;

                    card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                    card.Init(arrivedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);
                    if (areCardsScattered && arrivedCard.To != CardPlace.PlayedThisTurn)
                    {
                        var localPos = card.transform.localPosition;
                        localPos.x += Random.Range(-2f, 2f);
                        localPos.y += Random.Range(-2f, 2f);
                        card.transform.localPosition = localPos;
                        card.transform.Rotate(new Vector3(0, 0, Random.Range(-3f, 3f)), Space.Self);
                    }

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
            
            // Debug.Log("hand 3");

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
