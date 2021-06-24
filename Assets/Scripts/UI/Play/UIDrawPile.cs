using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIDrawPile : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

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
