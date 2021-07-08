using System;
using System.Collections.Generic;
using Solcery.Ruleset;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Solcery.UI.Create
{
    public class UIPlace : MonoBehaviour
    {
        public List<UIPlaceCard> Cards => _cards;
        public Dictionary<int, PlaceDisplayDataForPlayer> DisplayDatas => _displayDatas;
        public int PlaceId => _placeId;

        [SerializeField] private GameObject placeCardPrefab = null;
        [SerializeField] private Button deletePlaceButton = null;
        [SerializeField] private HorizontalLayoutGroup cardsLG = null;
        [SerializeField] private UIPlaceCard fakeCardBefore = null;
        [SerializeField] private UIPlaceCard fakeCardAfter = null;
        [SerializeField] private TMP_InputField placeIdInputField = null;

        private int _placeId;
        private List<UIPlaceCard> _cards;
        private Dictionary<int, PlaceDisplayDataForPlayer> _displayDatas;
        private Action _onRebuild;
        private Action<UIPlace> _onPointerEnterPlace, _onPointerExitPlace, _onDeletePlace;
        private UIPlaceCard _cardUnderPointer;
        private UIDroppableAreaOption _currentOption;

        public void Init(int initialPlaceId, Action onRebuild, Action<UIPlace> onPointerEnterPlace, Action<UIPlace> onPointerExitPlace, Action<UIPlace> onDeletePlace)
        {
            _placeId = initialPlaceId;

            _cards = new List<UIPlaceCard>();

            _onRebuild = onRebuild;
            _onPointerEnterPlace = onPointerEnterPlace;
            _onPointerExitPlace = onPointerExitPlace;
            _onDeletePlace = onDeletePlace;

            if (placeIdInputField != null)
                placeIdInputField.text = _placeId.ToString();

            fakeCardBefore?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            fakeCardAfter?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            deletePlaceButton.onClick.AddListener(() => onDeletePlace?.Invoke(this));
            placeIdInputField.onValueChanged.AddListener(OnPlaceIdValueChanged);
        }

        public void InitFromRulesetData(RulesetData rulesetData, PlaceData placeData, int initialPlaceId, Action onRebuild, Action<UIPlace> onPointerEnterPlace, Action<UIPlace> onPointerExitPlace, Action<UIPlace> onDeletePlace)
        {
            _placeId = placeData.PlaceId;
            foreach (var indexAmount in placeData.IndexAmount)
            {
                CreateCardFromIndexAmount(rulesetData, indexAmount);
            }


            _cards = new List<UIPlaceCard>();

            _onRebuild = onRebuild;
            _onPointerEnterPlace = onPointerEnterPlace;
            _onPointerExitPlace = onPointerExitPlace;
            _onDeletePlace = onDeletePlace;

            if (placeIdInputField != null)
                placeIdInputField.text = _placeId.ToString();

            fakeCardBefore?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            fakeCardAfter?.Init(null, null, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            deletePlaceButton.onClick.AddListener(() => onDeletePlace?.Invoke(this));
            placeIdInputField.onValueChanged.AddListener(OnPlaceIdValueChanged);
        }

        private void OnPlaceIdValueChanged(string newInput)
        {
            int.TryParse(newInput, out _placeId);
        }

        public void DeInit()
        {
            deletePlaceButton.onClick.RemoveAllListeners();
        }

        private void CreateCardFromIndexAmount(RulesetData rulesetData, CardIndexAmount indexAmount)
        {
            var placeCard = Instantiate(placeCardPrefab, cardsLG.transform).GetComponent<UIPlaceCard>();
            placeCard.InitFromRulesetData(rulesetData, indexAmount, DeleteCard, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
            placeCard.transform.SetSiblingIndex(_cards.Count + 1);
            _cards.Add(placeCard);
        }

        public void CreateCardOnDrop(CollectionCardType cardType)
        {
            var placeCard = Instantiate(placeCardPrefab, cardsLG.transform).GetComponent<UIPlaceCard>();

            placeCard.Init(cardType, DeleteCard, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);

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

            placeCard.transform.SetSiblingIndex(newCardIndex + 1);

            _cards.Insert(newCardIndex, placeCard);

            _onRebuild?.Invoke();
        }

        private void DeleteCard(UIPlaceCard card)
        {
            _cards.Remove(card);
            DestroyImmediate(card.gameObject);
            _onRebuild?.Invoke();
        }

        private void OnDroppableAreaPointerEnter(UIPlaceCard card, UIDroppableAreaOption option)
        {
            _cardUnderPointer = card;
            _currentOption = option;
            _onPointerEnterPlace?.Invoke(this);
        }

        private void OnDroppableAreaPointerExit(UIPlaceCard card, UIDroppableAreaOption option)
        {
            _cardUnderPointer = null;
            _onPointerExitPlace?.Invoke(this);
        }
    }
}
