using System.Collections.Generic;
using System.Threading;
using Solcery.Modules.Collection;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICollection : Singleton<UICollection>
    {
        [SerializeField] private LayoutElement le = null;
        [SerializeField] private Transform main = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private Button openButton = null;
        [SerializeField] private Button closeButton = null;

        private List<UICollectionCard> _cards;
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();
            _cards = new List<UICollectionCard>();

            openButton.onClick.AddListener(Open);
            closeButton.onClick.AddListener(Close);

            Reactives.Subscribe(Collection.Instance?.CollectionData, UpdateCollection, _cts.Token);
        }

        public void DeInit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        public void UpdateCollection(CollectionData collectionData)
        {
            DeleteAllCards();
            _cards = new List<UICollectionCard>();

            if (collectionData == null) return;

            foreach (var cardType in collectionData.CardTypes)
            {
                var newCard = Instantiate(cardPrefab, content).GetComponent<UICollectionCard>();
                newCard.Init(cardType);

                _cards.Add(newCard);
            }
        }

        private void Open()
        {
            le.preferredWidth = 300;
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            main.gameObject.SetActive(true);
        }

        private void Close()
        {
            le.preferredWidth = 0;
            closeButton.gameObject.SetActive(false);
            openButton.gameObject.SetActive(true);
            main.gameObject.SetActive(false);
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
