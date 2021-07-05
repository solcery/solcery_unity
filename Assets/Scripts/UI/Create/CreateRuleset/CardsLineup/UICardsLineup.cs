using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardsLineup : MonoBehaviour
    {
        [SerializeField] private GameObject lineupCardPrefab = null;

        [SerializeField] private HorizontalLayoutGroup cardsLG = null;

        [SerializeField] private UILineupCard fakeCardBefore = null;
        [SerializeField] private UILineupCard fakeCardAfter = null;

        private List<UILineupCard> _cards;
        private Action _onRebuild;
        private Action<UICardsLineup> _onPointerEnterLineup, _onPointerExitLineup;
        private UILineupCard _cardUnderPointer;
        private UIDroppableAreaOption _currentOption;

        public void Init(Action onRebuild, Action<UICardsLineup> onPointerEnterLineup, Action<UICardsLineup> onPointerExitLineup)
        {
            _onRebuild = onRebuild;
            _onPointerEnterLineup = onPointerEnterLineup;
            _onPointerExitLineup = onPointerExitLineup;

            _cards = new List<UILineupCard>();

            fakeCardBefore?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            fakeCardAfter?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
        }

        public void CreateCard(CollectionCardType cardType)
        {
            var lineUpCard = Instantiate(lineupCardPrefab, cardsLG.transform).GetComponent<UILineupCard>();

            lineUpCard.Init(cardType, DeleteCard, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);

            var cardUnderPointerIndex = _cards.Count > 0 ? _cards.IndexOf(_cardUnderPointer) : 0;
            var newCardIndex = _currentOption switch
            {
                UIDroppableAreaOption.Before => cardUnderPointerIndex,
                UIDroppableAreaOption.After => cardUnderPointerIndex + 1,
                _ => cardUnderPointerIndex
            };

            if (_cardUnderPointer == fakeCardBefore)
            {
                newCardIndex = 0;
            }
            if (_cardUnderPointer == fakeCardAfter)
            {
                newCardIndex = _cards.Count;
            }

            lineUpCard.transform.SetSiblingIndex(newCardIndex + 1);

            _cards.Insert(newCardIndex, lineUpCard);

            _onRebuild?.Invoke();
        }

        private void DeleteCard(UILineupCard card)
        {
            _cards.Remove(card);
            DestroyImmediate(card.gameObject);
            _onRebuild?.Invoke();
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
