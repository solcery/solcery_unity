using System;
using System.Collections.Generic;
using Solcery.Ruleset;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardsLineup : MonoBehaviour
    {
        public List<UILineupCard> Cards => _cards;
        public Dictionary<int, PlaceDisplayDataForPlayer> DisplayDatas => _displayDatas;

        [SerializeField] private GameObject lineupCardPrefab = null;
        [SerializeField] private Button deleteLineupButton = null;
        [SerializeField] private HorizontalLayoutGroup cardsLG = null;
        [SerializeField] private UILineupCard fakeCardBefore = null;
        [SerializeField] private UILineupCard fakeCardAfter = null;

        private List<UILineupCard> _cards;
        private Dictionary<int, PlaceDisplayDataForPlayer> _displayDatas;
        private Action _onRebuild;
        private Action<UICardsLineup> _onPointerEnterLineup, _onPointerExitLineup, _onDeleteLineup;
        private UILineupCard _cardUnderPointer;
        private UIDroppableAreaOption _currentOption;

        public void Init(Action onRebuild, Action<UICardsLineup> onPointerEnterLineup, Action<UICardsLineup> onPointerExitLineup, Action<UICardsLineup> onDeleteLineup)
        {
            _onRebuild = onRebuild;
            _onPointerEnterLineup = onPointerEnterLineup;
            _onPointerExitLineup = onPointerExitLineup;
            _onDeleteLineup = onDeleteLineup;

            _cards = new List<UILineupCard>();
            _displayDatas = new Dictionary<int, PlaceDisplayDataForPlayer>()
            {
                {0, new PlaceDisplayDataForPlayer()
                    {
                        IsVisible = true,
                        HorizontalAnchors = new PlaceDisplayAnchors(0, 1),
                        VecticalAnchors = new PlaceDisplayAnchors(0, 1),
                        CardLayoutOption = CardLayoutOption.LayedOut,
                        CardFaceOption = CardFaceOption.Up,
                    }
                },
                {1, new PlaceDisplayDataForPlayer()
                    {
                        IsVisible = false,
                        HorizontalAnchors = new PlaceDisplayAnchors(0.23f, 0.7f),
                        VecticalAnchors = new PlaceDisplayAnchors(0, 1),
                        CardLayoutOption = CardLayoutOption.LayedOut,
                        CardFaceOption = CardFaceOption.Up,
                    }
                }
            };

            fakeCardBefore?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            fakeCardAfter?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            deleteLineupButton.onClick.AddListener(() => onDeleteLineup?.Invoke(this));
        }

        public void DeInit()
        {
            deleteLineupButton.onClick.RemoveAllListeners();
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
