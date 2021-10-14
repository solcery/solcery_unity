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

                var cardClone = Instantiate<UIBoardCard>(cardToDelete, this.transform, true);
                cardClone.SetVisibility(true);
                cardClone.SetFaceDown(cardToDelete.IsFaceDown);
                // cardClone.ARF.enabled = false;
                cardClone.gameObject.name = "animated clone";
                var le = cardClone.gameObject.AddComponent<LayoutElement>();
                le.ignoreLayout = true;
                cardClone.MakeUnmaskable();
                // cardClone.transform.SetParent(destinationCardsParent, true);

                if (_clonedCards.ContainsKey(departedCard.CardData.CardId))
                    _clonedCards[departedCard.CardData.CardId] = cardClone;
                else
                    _clonedCards.Add(departedCard.CardData.CardId, cardClone);

                _cardsToAnimate.Add(departedCard);
            }
        }

        public bool HasSomethingToAnimate => (_cardsToAnimate != null && _cardsToAnimate.Count > 0);

        public async UniTask LaunchAll()
        {
            if (_cardsToAnimate == null || _cardsToAnimate.Count <= 0)
            {
                return;
            }

            var ct = this.GetCancellationTokenOnDestroy();

            List<UniTask> processingTasks = new List<UniTask>();

            foreach (var departedCard in _cardsToAnimate)
            {
                if (UIBoard.Instance.GetBoardPlace(departedCard.From, out var fromPlace) && UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
                {
                    if (_clonedCards.TryGetValue(departedCard.CardData.CardId, out var cardClone))
                    {
                        var cardId = departedCard.CardData.CardId;
                        var cardRect = cardClone.GetComponent<RectTransform>();
                        var cardSize = cardRect.rect.size;

                        var destination = toPlace.GetCardDestination(cardId);
                        // var rotation = toPlace.GetCardRotation(cardId);
                        var finalSize = toPlace.GetCardSize(cardId);

                        var tweenMove = cardClone?.transform?.DOMove(destination, 0.5f);
                        processingTasks.Add(ProcessMoveTask(tweenMove.WithCancellation(ct), cardClone.gameObject, toPlace, departedCard.CardData.CardId));

                        var scaleTo = new Vector3(finalSize.x / cardSize.x, finalSize.y / cardSize.y, 1);
                        if (scaleTo != Vector3.one)
                        {
                            var tweenScale = cardClone?.transform?.DOScale(scaleTo, 0.4f);
                            processingTasks.Add(ProcessScaleTask(tweenScale.WithCancellation(ct)));
                        }

                        if (fromPlace.AreCardsFaceDown != toPlace.AreCardsFaceDown)
                            cardClone?.TurnTheOtherWayAround();
                    }
                }
            }

            await UniTask.WhenAll(processingTasks);

            _cardsToAnimate = new List<BoardDataCardChangedPlace>();
            _clonedCards = new Dictionary<int, UIBoardCard>();
        }

        async UniTask ProcessMoveTask(UniTask task, GameObject go, IBoardPlace toPlace, int cardId)
        {
            await task;
            DestroyImmediate(go);
            toPlace.OnCardArrival(cardId);
        }

        async UniTask ProcessScaleTask(UniTask task)
        {
            await task;
        }
    }
}
