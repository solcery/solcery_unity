using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIPile : UIHand, IBoardPlace
    {
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, int cardsCount)
        {
            Debug.Log("UIPile UpdateWithDiv");
            // TODO count +- count here from each div
            if (cardsCount <= 0)
                this.gameObject.SetActive(false);
            else
            {
                this.gameObject.SetActive(true);
                if (cardsCountText != null) cardsCountText.text = cardsCount.ToString();
            }

            base.UpdateWithDiv(cardPlaceDiv, false, true, false);
        }

        protected override void OnCardCasted(int cardId)
        {

        }
    }
}
