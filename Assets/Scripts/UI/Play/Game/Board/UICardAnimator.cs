using Solcery.Utils;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.UI
{
    public class UICardAnimator : Singleton<UICardAnimator>
    {
        private List<BoardDataCardChangedPlace> _cardsToAnimate = new List<BoardDataCardChangedPlace>();
        private Dictionary<int, UIBoardCard> _clonedCards = new Dictionary<int, UIBoardCard>();

        public void Clear()
        {
            _cardsToAnimate = new List<BoardDataCardChangedPlace>();

            foreach (var idCard in _clonedCards)
            {
                DestroyImmediate(idCard.Value.gameObject);
            }
            _clonedCards = new Dictionary<int, UIBoardCard>();
        }

        public void Clone(UIBoardCard cardToDelete, BoardDataCardChangedPlace departedCard)
        {
            if (UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
            {
                var destinationCardsParent = toPlace.GetCardsParent();

                if (cardToDelete == null)
                    return;

                var cardToDeleteRect = cardToDelete.GetComponent<RectTransform>();
                Debug.Log($"cardToDelete size: {cardToDeleteRect.rect.size}");

                var cardClone = Instantiate<UIBoardCard>(cardToDelete, this.transform, true);
                cardClone.SetVisibility(true);
                cardClone.SetFaceDown(cardToDelete.IsFaceDown);
                // cardClone.ARF.enabled = false;
                cardClone.gameObject.name = "animated clone";
                var le = cardClone.gameObject.AddComponent<LayoutElement>();
                le.ignoreLayout = true;
                cardClone.MakeUnmaskable();
                cardClone.transform.SetParent(destinationCardsParent, true);

                if (_clonedCards.ContainsKey(departedCard.CardData.CardId))
                    _clonedCards[departedCard.CardData.CardId] = cardClone;
                else
                    _clonedCards.Add(departedCard.CardData.CardId, cardClone);

                _cardsToAnimate.Add(departedCard);
            }
        }

        public async UniTaskVoid LaunchAll()
        {
            UnityEngine.Debug.Log($"started: {UnityEngine.Time.realtimeSinceStartup}");

            var ct = this.GetCancellationTokenOnDestroy();

            List<UniTask> tasks = new List<UniTask>();
            // tasks.Add(transform.DOMoveX(10, 3).WithCancellation(ct));



            // await UniTask.WhenAll(
            //     transform.DOMoveX(10, 3).WithCancellation(ct),
            //     transform.DOScale(10, 3).WithCancellation(ct));


            foreach (var departedCard in _cardsToAnimate)
            {
                if (UIBoard.Instance.GetBoardPlace(departedCard.From, out var fromPlace) && UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
                {
                    if (_clonedCards.TryGetValue(departedCard.CardData.CardId, out var cardClone))
                    {
                        var cardId = departedCard.CardData.CardId;
                        var cardRect = cardClone.GetComponent<RectTransform>();
                        var cardSize = cardRect.rect.size;
                        Debug.Log($"size: {cardSize}");
                        var destination = toPlace.GetCardDestination(cardId);
                        // var rotation = toPlace.GetCardRotation(cardId);
                        var finalSize = toPlace.GetCardSize(cardId);
                        Debug.Log($"final size: {finalSize}");
                        var scaleTo = new Vector3(finalSize.x / cardSize.x, finalSize.y / cardSize.y, 1);
                        Debug.Log($"scale to: {scaleTo}");
                        var tweenMove = cardClone?.transform?.DOMove(destination, 0.5f);
                        var tweenScale = cardClone?.transform?.DOScale(scaleTo, 0.4f);
                        if (tweenMove != null)
                            tasks.Add(tweenMove.WithCancellation(ct));

                        if (fromPlace.AreCardsFaceDown != toPlace.AreCardsFaceDown)
                            cardClone?.PlayTurningAnimation();

                        tweenMove.OnComplete(() =>
                        {
                            DestroyImmediate(cardClone.gameObject);
                            _clonedCards.Remove(departedCard.CardData.CardId);
                            toPlace.OnCardArrival(departedCard.CardData.CardId);
                        });
                    }
                }
            }

            await UniTask.WhenAll(tasks);
            UnityEngine.Debug.Log($"finished: {UnityEngine.Time.realtimeSinceStartup}");

            _cardsToAnimate = new List<BoardDataCardChangedPlace>();
            _clonedCards = new Dictionary<int, UIBoardCard>();
        }
    }
}
