using System.Collections.Generic;
using System.Threading;
using Solcery.Modules.Collection;
using Solcery.Utils.Reactives;
using Solcery.WebGL;
using UnityEngine;

namespace Solcery.UI
{
    public class UICardCollection : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject cardPrefab;

        private List<UICard> _cards;
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();
            _cards = new List<UICard>();

            Reactives.SubscribeTo(Collection.Instance?.CollectionData, UpdateCollection, _cts.Token);
        }

        public void DeInit()
        {
            _cts.Cancel();
        }

        public void UpdateCollection(CollectionData collectionData)
        {
            DeleteAllCards();
            _cards = new List<UICard>();

            if (collectionData == null) return;

            foreach (var cardData in collectionData.Cards)
            {
                var newCard = Instantiate(cardPrefab, content).GetComponent<UICard>();
                newCard.Init(cardData, OnCardCasted);

                _cards.Add(newCard);
            }
        }

        private void OnCardCasted(string cardMintAddress)
        {
            UnityToReact.Instance?.CallUseCard(cardMintAddress);
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
