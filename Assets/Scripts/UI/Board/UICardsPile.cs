using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UICardsPile : MonoBehaviour, IBoardPlace
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private TextMeshProUGUI cardsCountText = null;

        public bool AreCardsFaceDown => true;

        public Vector3 GetCardDestination(int cardId)
        {
            return content.position;
        }

        public Transform GetCardsParent()
        {
            return content;
        }

        public void OnCardArrival(int cardId)
        {

        }

        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, int cardsCount)
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
