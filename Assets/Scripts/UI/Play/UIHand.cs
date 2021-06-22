using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIHand : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private Transform content = null;

        private List<UICard> _cards;

        protected void UpdateCards(List<CardData> cards, bool areButtonsInteractable)
        {
            DeleteAllCards();

            if (cards == null)
                return;

            foreach (var cardData in cards)
            {
                var card = Instantiate(cardPrefab, content).GetComponent<UICard>();
                card.Init(cardData, areButtonsInteractable, OnCardCasted);

                _cards.Add(card);
            }
        }

        protected virtual void OnCardCasted(string cardMintAddress, int cardIndex)
        {
            Debug.Log("interactable card clicked");
        }

        public void DeleteAllCards()
        {
            if (_cards != null && _cards.Count > 0)
            {
                for (int i = _cards.Count - 1; i >= 0; i--)
                {
                    _cards[i].DeInit();
                    DestroyImmediate(_cards[i].gameObject);
                }
            }

            _cards = new List<UICard>();
        }
    }
}
