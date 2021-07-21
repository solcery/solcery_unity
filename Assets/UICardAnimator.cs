using Solcery.UI;
using Solcery.UI.Play;
using Solcery.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery
{
    public class UICardAnimator : Singleton<UICardAnimator>
    {
        public void StartAnimating(UIBoardCard cardToDelete, BoardDataCardChangedPlace departedCard)
        {
            if (UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
            {
                var destination = toPlace.GetCardDestination(departedCard.CardData.CardId);
                var destinationCardsParent = toPlace.GetCardsParent();
                var cardClone = Instantiate<UIBoardCard>(cardToDelete, this.transform, true);
                cardClone.gameObject.name = "1";
                var le = cardClone.gameObject.AddComponent<LayoutElement>();
                le.ignoreLayout = true;
                cardClone.transform.SetParent(destinationCardsParent, true);
                // cardClone.transform.SetParent(destinationCardsParent);
                // cardClone.GetComponent<Animator>().SetTrigger("Moving");
                var tween = cardClone.transform.DOMove(destination.position, 1f);

                tween.OnComplete(() =>
                {
                    UnityEngine.Debug.Log("Tween complete");
                    Debug.Log(cardClone.GetComponent<LayoutElement>().ignoreLayout);
                    DestroyImmediate(cardClone.gameObject);
                    toPlace.OnCardArrival(departedCard.CardData.CardId);
                });
            }
        }
    }
}
