using System.Collections.Generic;
using Solcery.Modules;
using Solcery.React;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIHand : MonoBehaviour, IBoardPlace
    {
        public bool AreCardsFaceDown => _areCardsFaceDown;

        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        private PlaceDisplayData _displayData;

        [SerializeField] protected GameObject cardPrefab = null;
        [SerializeField] protected Transform content = null;

        protected bool _areCardsFaceDown;
        private int _cardsToArrive;

        protected Dictionary<int, UIBoardCard> _cardsById;

        private bool _hideAllButTop;

        public void Clear()
        {
            DeleteAllCards();
        }

        public void UpdateGameContent(GameContent gameContent)
        {
            foreach (var card in _cardsById.Values)
            {
                card.UpdateGameContent(gameContent);
            }
        }

        public void UpdateWithDiff(GameContent gameContent, CardPlaceDiff cardPlaceDiff, bool areCardsInteractable, bool areCardsFaceDown, bool showCoins, bool areCardsScattered = false, bool hideAllButTop = false)
        {
            _areCardsFaceDown = areCardsFaceDown;
            _hideAllButTop = hideAllButTop;

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
                for (int i = 0; i < cardPlaceDiff.Stayed.Count; i++)
                {
                    var stayedCard = cardPlaceDiff.Stayed[i];

                    if (!_cardsById.ContainsKey(stayedCard.CardData.CardId))
                    {
                        var card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                        card.Init(gameContent, stayedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);

                        // if (hideAllButTop && i != cardPlaceDiff.Stayed.Count - 1)
                        //     card.SetVisibility(false);

                        _cardsById.Add(stayedCard.CardData.CardId, card);
                    }
                    else if (_cardsById.TryGetValue(stayedCard.CardData.CardId, out var existringCard))
                    {
                        existringCard.StopShaking();
                        if (hideAllButTop && i != cardPlaceDiff.Stayed.Count - 1)
                            existringCard.SetVisibility(false);
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

            if (cardPlaceDiff.Arrived != null && cardPlaceDiff.Arrived.Count > 0)
            {
                _cardsToArrive = 0;

                foreach (var arrivedCard in cardPlaceDiff.Arrived)
                {
                    UIBoardCard card;

                    card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
                    card.Init(gameContent, arrivedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted);

                    if (arrivedCard.From == 0 || !UIBoard.Instance.GetBoardPlace(arrivedCard.From, out var fromPlace))
                        card.SetVisibility(true);
                    else
                    {
                        _cardsToArrive += 1;
                        card.SetVisibility(false);
                    }

                    if (_cardsById.ContainsKey(arrivedCard.CardData.CardId))
                        _cardsById[arrivedCard.CardData.CardId] = card;
                    else
                        _cardsById.Add(arrivedCard.CardData.CardId, card);
                }

                if (_cardsToArrive <= 0 && _hideAllButTop)
                    HideAllButTop();
            }
            else if (_hideAllButTop)
            {
                HideAllButTop();
            }


            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        }

        public void OnCardArrival(int cardId)
        {
            _cardsToArrive -= 1;

            var card = GetCardById(cardId);
            if (card != null) card.SetVisibility(true);

            if (_cardsToArrive <= 0 && _hideAllButTop)
            {
                HideAllButTop();
                _cardsToArrive = 0;
            }
        }

        private void HideAllButTop()
        {
            var childCount = content.childCount;

            if (childCount > 0)
            {
                var lastCard = content?.GetChild(childCount - 1)?.GetComponent<UIBoardCard>();
                var lastCardId = lastCard?.CardData?.CardId;

                foreach (var pair in _cardsById)
                {
                    if (pair.Key != lastCardId)
                        pair.Value?.SetVisibility(false);
                }

                lastCard.SetVisibility(true);
            }
        }

        protected virtual void OnCardCasted(int cardId)
        {
            UnityToReact.Instance?.CallCastCard(cardId);
        }

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

            return this.transform.position;
        }

        public Vector3 GetCardRotation(int cardId)
        {
            var card = GetCardById(cardId);

            if (card != null)
                return card.transform.localRotation.eulerAngles;


            return this.transform.localRotation.eulerAngles;
        }

        public Vector2 GetCardSize(int cardId)
        {
            var card = GetCardById(cardId);

            if (card != null)
            {
                var rect = card?.GetComponent<RectTransform>();
                if (rect != null)
                    return rect.rect.size;
            }

            return Vector2.one;
        }

        public Transform GetCardsParent()
        {
            return content;
        }
    }
}
