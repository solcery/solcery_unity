using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIPile : UIHand
    {
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

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
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
                if (cardsCountText != null) cardsCountText.text = cardsCount.ToString();
            }

            base.UpdateWithDiff(cardPlaceDiff, false, true, false, true);
        }

        protected override void OnCardCasted(int cardId)
        {

        }
    }
}
