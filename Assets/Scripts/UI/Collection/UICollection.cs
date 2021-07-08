using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.FSM.Create;
using Solcery.Modules.Collection;
using Solcery.UI.Create;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICollection : Singleton<UICollection>
    {
        [SerializeField] private UICollectionCardDragger dragger = null;
        [SerializeField] private LayoutElement le = null;
        [SerializeField] private Transform main = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private Button openButton = null;
        [SerializeField] private Button closeButton = null;
        [SerializeField] private Button createNewCardButton = null;
        [SerializeField] private CreateTransition fromRulesetToCard = null;

        private List<UICollectionCard> _cards;
        private CancellationTokenSource _cts;
        private UICollectionMode _mode;
        private Action _onRebuild;

        public void Init(Canvas createCanvas, Action onRebuild)
        {
            _cts = new CancellationTokenSource();
            _cards = new List<UICollectionCard>();

            _onRebuild = onRebuild;

            dragger?.Init(createCanvas);

            openButton.onClick.AddListener(Open);
            closeButton.onClick.AddListener(Close);

            createNewCardButton.onClick.AddListener(async () => await CreateNewCard());

            Reactives.Subscribe(Collection.Instance?.CollectionData, UpdateCollection, _cts.Token);
        }

        private async UniTask CreateNewCard()
        {
            switch (_mode)
            {
                case UICollectionMode.CreateCard:
                    break;
                case UICollectionMode.CreateRuleset:
                    UICreate.Instance.Tabs.OnTabClicked(1);
                    await CreateSM.Instance.PerformTransition(fromRulesetToCard);
                    break;
            }

            UICreateCard.Instance.CreateNewCard();
        }

        public void DeInit()
        {
            dragger?.DeInit();

            _cts.Cancel();
            _cts.Dispose();
        }

        public void UpdateCollection(CollectionData collectionData)
        {
            DeleteAllCards();
            _cards = new List<UICollectionCard>();

            if (collectionData == null) return;

            for (int i = 0; i < collectionData.CardTypes.Count; i++)
            {
                var newCard = Instantiate(cardPrefab, content).GetComponent<UICollectionCard>();
                newCard.Init(collectionData.CardTypes[i], i, OnCardClicked, OnPointerDown);

                _cards.Add(newCard);
            }
        }

        public void SetMode(UICollectionMode mode)
        {
            _mode = mode;
        }

        private void OnCardClicked(int cardIndex)
        {
            switch (_mode)
            {
                case UICollectionMode.CreateCard:
                    UICreateCard.Instance?.OpenCard(_cards[cardIndex].CardType);
                    break;
                case UICollectionMode.CreateRuleset:
                    break;
            }
        }

        private void OnPointerDown(int cardIndex)
        {
            switch (_mode)
            {
                case UICollectionMode.CreateCard:
                    break;
                case UICollectionMode.CreateRuleset:
                    dragger.StartDragging(_cards[cardIndex]);
                    break;
            }
        }

        public void Open()
        {
            le.preferredWidth = 300;
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            main.gameObject.SetActive(true);

            _onRebuild?.Invoke();
        }

        private void Close()
        {
            le.preferredWidth = 0;
            closeButton.gameObject.SetActive(false);
            openButton.gameObject.SetActive(true);
            main.gameObject.SetActive(false);

            _onRebuild?.Invoke();
        }

        private void DeleteAllCards()
        {
            if (_cards == null || _cards.Count <= 0)
                return;

            for (int i = _cards.Count - 1; i >= 0; i--)
            {
                _cards[i].DeInit();
                DestroyImmediate(_cards[i].gameObject);
            }
        }
    }
}
