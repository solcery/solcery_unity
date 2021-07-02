using System;
using Solcery.Modules.Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIBoardCard : MonoBehaviour
    {
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        private BoardCardData _cardData;
        private BoardCardType _cardType;

        public void Init(BoardCardData cardData, bool isInteractable, Action<string, int> onCardCasted = null)
        {
            _cardData = cardData;
            _cardType = Board.Instance.BoardData.Value.GetCardType(_cardData.CardTypeId);

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoinsCount(_cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SubsribeToButton(isInteractable, onCardCasted);
            }
            else
            {
                SetName("unknown card type!");
                SetDescription("unknown card type!");
            }
        }

        public void DeInit()
        {
            button.onClick.RemoveAllListeners();
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

        private void SubsribeToButton(bool isInteractable, Action<string, int> onCardCasted = null)
        {
            if (button != null)
            {
                button.interactable = isInteractable;

                if (isInteractable)
                    button.onClick.AddListener(() =>
                    {
                        onCardCasted?.Invoke(_cardType.MintAddress, _cardData.CardId);
                    });
            }
        }
    }
}
