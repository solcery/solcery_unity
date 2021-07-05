using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UILineupCard : MonoBehaviour
    {
        public UILineupCardData Data { get; private set; }

        [SerializeField] private UIDroppableArea before = null;
        [SerializeField] private UIDroppableArea after = null;
        [SerializeField] private UILineupCardAmountSwitcher amountSwitcher = null;
        [SerializeField] private Button deleteButton = null;
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        private Action<UILineupCard> _onDelete;

        public void Init(CollectionCardType cardType, Action<UILineupCard> onDelete, Action<UILineupCard, UIDroppableAreaOption> onPointerEnter, Action<UILineupCard, UIDroppableAreaOption> onPointerExit)
        {
            Data = new UILineupCardData(cardType, 1);
            _onDelete = onDelete;

            before?.Init(this, UIDroppableAreaOption.Before, onPointerEnter, onPointerExit);
            after?.Init(this, UIDroppableAreaOption.After, onPointerEnter, onPointerExit);
            amountSwitcher?.Init(Data.Amount, (newAmount) => Data.Amount = newAmount);
            deleteButton?.onClick.AddListener(DeleteCard);

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

        private void DeleteCard()
        {
            _onDelete?.Invoke(this);
        }
    }
}
