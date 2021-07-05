using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardsLineup : MonoBehaviour
    {
        [SerializeField] private GameObject lineupCardPrefab = null;
        // [SerializeField] private GameObject droppableAreaPrefab = null;

        [SerializeField] private HorizontalLayoutGroup cardsLG = null;
        // [SerializeField] private HorizontalLayoutGroup droppablesLG = null;

        [SerializeField] private UILineupCard fakeCardBefore = null;
        [SerializeField] private UILineupCard fakeCardAfter = null;
        // [SerializeField] private UIDroppableArea fakeGlobalBefore = null;
        // [SerializeField] private UIDroppableArea fakeGlobalAfter = null;

        private List<UILineupCard> _cards;
        // private List<UIDroppableArea> _droppables;
        private RectTransform _rebuildOnChange;
        private Action<UICardsLineup> _onPointerEnterLineup, _onPointerExitLineup;
        private UILineupCard _cardUnderPointer;
        private UIDroppableAreaOption _currentOption;

        public void Init(RectTransform rebuildOnChange, Action<UICardsLineup> onPointerEnterLineup, Action<UICardsLineup> onPointerExitLineup)
        {
            _rebuildOnChange = rebuildOnChange;
            _onPointerEnterLineup = onPointerEnterLineup;
            _onPointerExitLineup = onPointerExitLineup;

            _cards = new List<UILineupCard>();
            // _droppables = new List<UIDroppableArea>();

            fakeCardBefore?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            fakeCardAfter?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);

            // fakeGlobalBefore.Init(fakeCardBefore, UIDroppableAreaOption.Before, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            // fakeGlobalAfter.Init(fakeCardAfter, UIDroppableAreaOption.After, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
        }

        public void CreateCard(CollectionCardType cardType)
        {
            var lineUpCard = Instantiate(lineupCardPrefab, cardsLG.transform).GetComponent<UILineupCard>();
            // var before = Instantiate(droppableAreaPrefab, droppablesLG.transform).GetComponent<UIDroppableArea>();
            // var after = Instantiate(droppableAreaPrefab, droppablesLG.transform).GetComponent<UIDroppableArea>();

            lineUpCard.Init(cardType, DeleteCard, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            // before.Init(lineUpCard, UIDroppableAreaOption.Before, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            // after.Init(lineUpCard, UIDroppableAreaOption.After, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);

            var cardUnderPointerIndex = _cards.Count > 0 ? _cards.IndexOf(_cardUnderPointer) : 0;
            var newCardIndex = _currentOption switch
            {
                UIDroppableAreaOption.Before => cardUnderPointerIndex,
                UIDroppableAreaOption.After => cardUnderPointerIndex + 1,
                _ => cardUnderPointerIndex
            };

            if (_cardUnderPointer == fakeCardBefore)
            {
                Debug.Log("fakeCardBefore");
                newCardIndex = 0;
            }
            if (_cardUnderPointer == fakeCardAfter)
            {
                Debug.Log("fakeCardAfter");
                newCardIndex = _cards.Count;
            }

            lineUpCard.transform.SetSiblingIndex(newCardIndex + 1);
            // before.transform.SetSiblingIndex(1 + newCardIndex * 2);
            // after.transform.SetSiblingIndex(1 + newCardIndex * 2 + 1);

            _cards.Insert(newCardIndex, lineUpCard);
            // _droppables.Insert(newCardIndex * 2, before);
            // _droppables.Insert(newCardIndex * 2 + 1, after);

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rebuildOnChange);
        }

        private void DeleteCard(UILineupCard card)
        {
            _cards.Remove(card);
            DestroyImmediate(card.gameObject);
        }

        private void OnDroppableAreaPointerEnter(UILineupCard card, UIDroppableAreaOption option)
        {
            _cardUnderPointer = card;
            _currentOption = option;
            _onPointerEnterLineup?.Invoke(this);
        }

        private void OnDroppableAreaPointerExit(UILineupCard card, UIDroppableAreaOption option)
        {
            _cardUnderPointer = null;
            _onPointerExitLineup?.Invoke(this);
        }
    }
}
