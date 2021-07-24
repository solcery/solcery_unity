using Solcery.UI;
using Solcery.UI.Play;
using Solcery.Utils;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Solcery
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

                var cardClone = Instantiate<UIBoardCard>(cardToDelete, this.transform, true);
                cardClone.SetFaceDown(cardToDelete.IsFaceDown);
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

        public void LaunchAll()
        {
            foreach (var departedCard in _cardsToAnimate)
            {
                if (UIBoard.Instance.GetBoardPlace(departedCard.From, out var fromPlace) && UIBoard.Instance.GetBoardPlace(departedCard.To, out var toPlace))
                {
                    if (_clonedCards.TryGetValue(departedCard.CardData.CardId, out var cardClone))
                    {
                        var destination = toPlace.GetCardDestination(departedCard.CardData.CardId);
                        var rotation = toPlace.GetCardRotation(departedCard.CardData.CardId);
                        var tween = cardClone.transform.DOMove(destination, 0.5f);
                        cardClone.transform.DORotate(rotation, 0.25f);

                        if (fromPlace.AreCardsFaceDown != toPlace.AreCardsFaceDown)
                            cardClone?.PlayTurningAnimation();

                        tween.OnComplete(() =>
                        {
                            DestroyImmediate(cardClone.gameObject);
                            toPlace.OnCardArrival(departedCard.CardData.CardId);
                            _clonedCards.Remove(departedCard.CardData.CardId);
                        });
                    }
                }
            }

            _cardsToAnimate = new List<BoardDataCardChangedPlace>();
            _clonedCards = new Dictionary<int, UIBoardCard>();
        }
    }
}
