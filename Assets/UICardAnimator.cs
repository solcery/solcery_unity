using Solcery.UI;
using Solcery.UI.Play;
using Solcery.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Solcery
{
    public class UICardAnimator : Singleton<UICardAnimator>
    {
        public GameObject targetPrefab = null;

        private List<BoardDataCardChangedPlace> _cardsToAnimate = new List<BoardDataCardChangedPlace>();
        private Dictionary<int, GameObject> _clonedCards = new Dictionary<int, GameObject>();

        public void Clone(UIBoardCard cardToDelete, BoardDataCardChangedPlace departedCard)
        {
            if (UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
            {
                var destinationCardsParent = toPlace.GetCardsParent();
                var cardClone = Instantiate<UIBoardCard>(cardToDelete, this.transform, true);

                var target = Instantiate(targetPrefab, this.transform);
                target.transform.position = cardToDelete.transform.position;

                cardClone.gameObject.name = "1";
                var le = cardClone.gameObject.AddComponent<LayoutElement>();
                le.ignoreLayout = true;
                cardClone.transform.SetParent(destinationCardsParent, true);

                if (_clonedCards.ContainsKey(departedCard.CardData.CardId))
                    _clonedCards[departedCard.CardData.CardId] = cardClone.gameObject;
                else
                    _clonedCards.Add(departedCard.CardData.CardId, cardClone.gameObject);

                _cardsToAnimate.Add(departedCard);
                // cardClone.GetComponent<Animator>().SetTrigger("Moving");
            }
        }

        public void LaunchAll()
        {
            foreach (var departedCard in _cardsToAnimate)
            {
                if (UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
                {
                    if (_clonedCards.TryGetValue(departedCard.CardData.CardId, out var cardClone))
                    {
                        var destination = toPlace.GetCardDestination(departedCard.CardData.CardId);
                        var target = Instantiate(targetPrefab, this.transform);
                        target.transform.position = destination;

                        var tween = cardClone.transform.DOMove(destination, 1f);

                        tween.OnComplete(() =>
                        {
                            UnityEngine.Debug.Log("Tween complete");
                            Debug.Log(cardClone.GetComponent<LayoutElement>().ignoreLayout);
                            DestroyImmediate(cardClone.gameObject);
                            toPlace.OnCardArrival(departedCard.CardData.CardId);
                            _clonedCards.Remove(departedCard.CardData.CardId);
                        });
                    }
                }
            }

            _cardsToAnimate = new List<BoardDataCardChangedPlace>();
            _clonedCards = new Dictionary<int, GameObject>();
        }
    }
}
