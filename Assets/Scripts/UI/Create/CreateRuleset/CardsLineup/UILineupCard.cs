using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UILineupCard : MonoBehaviour
    {
        public UILineupCardData Data { get; private set; }
        public UIDroppableArea Before { get; private set; }
        public UIDroppableArea After { get; private set; }

        [SerializeField] private UILineupCardAmountSwitcher amountSwitcher = null;
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        public void Init(CollectionCardType cardType, UIDroppableArea before, UIDroppableArea after)
        {
            Data = new UILineupCardData(cardType, 1);

            Before = before;
            After = after;

            amountSwitcher?.Init(Data.Amount, (newAmount) => { Data.Amount = newAmount; Debug.Log(Data.Amount); });

            ApplyCardType();
        }

        private void ApplyCardType()
        {
            if (Data.CardType != null)
            {
                SetPicture(Data.CardType.Metadata.Picture);
                SetCoinsCount(Data.CardType.Metadata.Coins);
                SetName(Data.CardType.Metadata.Name);
                SetDescription(Data.CardType.Metadata.Description);
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
