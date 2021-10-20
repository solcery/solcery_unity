using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Solcery.UI
{
    public class UIPile : UIHand
    {
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

        private int _currentCardsCount;
        private CardPlaceDiff _cardPlaceDiff;

        public new void Clear()
        {
            if (cardsCountText != null) cardsCountText.text = string.Empty;
            base.Clear();
        }

        public void UpdateWithDiff(PlaceDisplayData displayData, GameContent gameContent, CardPlaceDiff cardPlaceDiff, int cardsCount, bool areCardsInteractable, bool areCardsFaceDown)
        {
            _cardPlaceDiff = cardPlaceDiff;

            if (areCardsFaceDown)
            {
                if (cardsCountText != null && cardsCountText.gameObject != null)
                    cardsCountText.gameObject.SetActive(true);
                if (_currentCardsCount != cardsCount || (cardPlaceDiff != null && ((cardPlaceDiff.Arrived != null && cardPlaceDiff.Arrived.Count > 0) || (cardPlaceDiff.Departed != null && cardPlaceDiff.Departed.Count > 0))))
                    SetCardsCountText(cardsCount).Forget();
            }
            else
            {
                if (cardsCountText != null && cardsCountText.gameObject != null)
                    cardsCountText.gameObject.SetActive(false);
            }

            _currentCardsCount = cardsCount;

            base.UpdateWithDiff(displayData, gameContent, cardPlaceDiff, areCardsInteractable, areCardsFaceDown, false, true, true);
        }

        private async UniTaskVoid SetCardsCountText(int newCardsCount)
        {
            if (cardsCountText != null)
            {
                if (newCardsCount <= 0)
                    cardsCountText.text = string.Empty;
                else
                {
                    cardsCountText.gameObject?.SetActive(false);

                    await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

                    if (cardsCountText != null)
                    {
                        cardsCountText.text = newCardsCount.ToString();
                        cardsCountText.gameObject?.SetActive(true);
                    }
                }
            }
        }

        // protected override void OnCardCasted(int cardId)
        // {

        // }
    }
}
