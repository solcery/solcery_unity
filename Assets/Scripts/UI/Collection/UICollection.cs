using System.Collections.Generic;
using System.Threading;
using Solcery.Modules.Collection;
using Solcery.Utils.Reactives;
using Solcery.WebGL;
using UnityEngine;

namespace Solcery.UI
{
    public class UICollection : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject cardPrefab;

        private List<UICollectionCard> _cards;
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();
            _cards = new List<UICollectionCard>();

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

        private void OnCardCasted(string cardMintAddress, int cardIndex)
        {
            // UnityToReact.Instance?.CallUseCard(cardMintAddress);
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
