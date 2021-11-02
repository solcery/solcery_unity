using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private CanvasGroup cg = null;
        [SerializeField] protected Image bgImage = null;
        [SerializeField] protected GameObject cardPrefab = null;
        [SerializeField] protected Transform content = null;
        [SerializeField] protected RectTransform contentRect = null;
        [SerializeField] protected RectTransform highlightedRect = null;

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
            if (_cardsById != null && _cardsById.Count > 0)
            {
                foreach (var card in _cardsById.Values)
                {
                    card?.UpdateGameContent(gameContent);
                }
            }
        }

        public void UpdateWithDiff(PlaceDisplayData displayData, GameContent gameContent, CardPlaceDiff cardPlaceDiff, bool areCardsInteractable, bool areCardsFaceDown, bool showCoins, bool areCardsScattered = false, bool hideAllButTop = false)
        {
            _displayData = displayData;
            _areCardsFaceDown = areCardsFaceDown;
            _hideAllButTop = hideAllButTop;

            SetBgColor();
            SetAlpha();

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
                        card.Init(gameContent, stayedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted, OnCardHighlighted, OnCardDehighlighted);

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
                    if (departedCard.CardData == null) continue;
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
                    card.Init(gameContent, arrivedCard.CardData, _areCardsFaceDown, areCardsInteractable, showCoins, OnCardCasted, OnCardHighlighted, OnCardDehighlighted);

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


            Rebuild();
            // LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
            // LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
            // LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        }

        // private int _siblingIndex;
        private Dictionary<int, int> _cardIdSibling;

        private void OnCardHighlighted(int cardId)
        {
            if (_cardsById.TryGetValue(cardId, out var card))
            {
                card?.transform?.SetParent(highlightedRect.transform, true);
            }
        }

        private void OnCardDehighlighted(int cardId)
        {
            if (_cardsById.TryGetValue(cardId, out var card))
            {
                card?.transform?.SetParent(contentRect.transform, true);
                //TODO: set same sibling as before highlighting

                if (_cardIdSibling.TryGetValue(cardId, out var siblingInex))
                {
                    card.transform.SetSiblingIndex(siblingInex);
                }
            }
        }

        private void Rebuild()
        {
            _cardIdSibling = new Dictionary<int, int>();

            var contentWidth = contentRect.rect.width;
            // Debug.Log(contentWidth);

            var cardsCount = _cardsById.Count;

            if (cardsCount > 0)
            {
                var anyCard = _cardsById.First().Value;
                var cardRect = anyCard.transform as RectTransform;
                var cardWidth = cardRect.rect.width;
                // Debug.Log(cardWidth);

                var gapWidth = (contentWidth - cardWidth * (cardsCount + 0.4f)) / (cardsCount - 1);
                // Debug.Log(gapWidth);

                for (int i = 0; i < cardsCount; i++)
                {
                    var item = _cardsById.ElementAt(i);
                    var cardId = item.Key;
                    var card = item.Value;
                    var currentCardPos = card.transform.localPosition;

                    var padding = cardWidth * 0.2f;

                    var newCardX = padding + (i + 0.5f) * cardWidth + i * gapWidth;
                    // Debug.Log(newCardX);
                    card.transform.localPosition = new Vector2(newCardX, currentCardPos.y);

                    _cardIdSibling[cardId] = i;
                }
            }
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

        private void SetAlpha()
        {
            if (cg == null)
                return;

            var isAlphaZero = _displayData.Alpha == 0;
            cg.interactable = !isAlphaZero;
            cg.blocksRaycasts = !isAlphaZero;

            float alpha = (float)_displayData.Alpha / 100;
            cg.alpha = alpha;
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
