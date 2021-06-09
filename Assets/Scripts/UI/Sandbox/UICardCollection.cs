using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Solcery.Modules.CardCollection;
using Solcery.WebGL;
using UnityEngine;

namespace Solcery.UI
{
    public class UICardCollection : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject cardPrefab;

        private List<UICard> _cards;

        public void Init()
        {
            _cards = new List<UICard>();

            CardCollection.Instance?.Collection.ForEachAsync(c =>
            {
                UpdateCollection(c);
            }, this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void DeInit()
        {

        }

        public void UpdateCollection(Collection collection)
        {
            DeleteAllCards();
            _cards = new List<UICard>();

            if (collection == null) return;

            foreach (var cardData in collection.Cards)
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
