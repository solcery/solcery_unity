using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UICardsPile : MonoBehaviour, IBoardPlace
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

        public Transform GetCardDestination(int cardId)
        {
            return content;
        }

        public Transform GetCardsParent()
        {
            return content;
        }

        public void OnCardArrival(int cardId)
        {

        }

        public void SetCardsCount(int cardsCount)
        {
            if (cardsCount <= 0)
                this.gameObject.SetActive(false);
            else
            {
                this.gameObject.SetActive(true);
                if (cardsCountText != null) cardsCountText.text = cardsCount.ToString();
            }
        }
    }
}
