using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UILineupCard : MonoBehaviour
    {
        public UIDroppableArea Before { get; private set; }
        public UIDroppableArea After { get; private set; }

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        public void Init(CollectionCardType cardType, UIDroppableArea before, UIDroppableArea after)
        {
            Before = before;
            After = after;

            if (cardType != null)
            {
                SetPicture(cardType.Metadata.Picture);
                SetCoinsCount(cardType.Metadata.Coins);
                SetName(cardType.Metadata.Name);
                SetDescription(cardType.Metadata.Description);
            }
            else
            {
                SetName("unknown card type!");
                SetDescription("unknown card type!");
            }
        }

        private void SetPicture(int picture)
        {
            if (cardPicture != null)
                cardPicture.sprite = cardPictures.GetSpriteByIndex(picture);
        }

        private void SetCoinsCount(int coinsCount)
        {
            if (cardCoinsCount != null)
                cardCoinsCount.text = coinsCount.ToString();
        }

        private void SetName(string name)
        {
            if (cardName != null)
                cardName.text = name;
        }

        private void SetDescription(string description)
        {
            if (cardDescription != null)
                cardDescription.text = description;
        }
    }
}
