using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Solcery.UI.Play.Game.Board
{
    public class UIPile : UIHand
    {
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

        private int _currentCardsCount;

        public new void Clear()
        {
            if (cardsCountText != null) cardsCountText.text = string.Empty;
            base.Clear();
        }

        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff, int cardsCount)
        {
            // TODO: count +- count here from each diff
            if (cardsCount <= 0)
            {
                this.gameObject?.SetActive(false);
            }
            else
            {
                this.gameObject?.SetActive(true);

                if (_currentCardsCount != cardsCount || (cardPlaceDiff != null && ((cardPlaceDiff.Arrived != null && cardPlaceDiff.Arrived.Count > 0) || (cardPlaceDiff.Departed != null && cardPlaceDiff.Departed.Count > 0))))
                    SetCardsCountText(cardsCount).Forget();
            }

            _currentCardsCount = cardsCount;

            base.UpdateWithDiff(cardPlaceDiff, false, true, false, true, true);
        }

        private async UniTaskVoid SetCardsCountText(int newCardsCount)
        {
            if (cardsCountText != null)
            {
                cardsCountText.gameObject?.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                cardsCountText.text = newCardsCount.ToString();
                cardsCountText.gameObject?.SetActive(true);
            }
        }

        protected override void OnCardCasted(int cardId)
        {

        }
    }
}
