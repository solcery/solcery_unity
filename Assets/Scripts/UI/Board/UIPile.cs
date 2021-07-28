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

        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, int cardsCount)
        {
            // TODO: count +- count here from each div
            if (cardsCount <= 0) 
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
                if (cardsCountText != null) cardsCountText.text = cardsCount.ToString();
            }

            base.UpdateWithDiv(cardPlaceDiv, false, true, false, true);
            if (content.childCount > 0) {
                var lastChild = content.GetChild(content.childCount - 1);
                cardsCountText.transform.localPosition = lastChild.transform.localPosition;
            }
        }

        protected override void OnCardCasted(int cardId)
        {

        }
    }
}
