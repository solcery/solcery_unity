using System;
using Solcery.Modules.Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIBoardCard : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] private UIBoardCardPointerHandler pointerHandler = null;
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private GameObject cardCoins = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        private BoardCardData _cardData;
        private BoardCardType _cardType;
        private Action<int> _onCardCasted;

        private bool _hasBeenClicked = false;

        public void Init(BoardCardData cardData, bool isInteractable, bool showCoins = false, Action<int> onCardCasted = null)
        {
            _cardData = cardData;
            _cardType = Board.Instance.BoardData.Value.GetCardType(_cardData.CardType);
            _onCardCasted = onCardCasted;

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoins(showCoins, _cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SubsribeToButton(isInteractable);
            }
            else
            {
                Debug.Log("BoardCardType is null");
                SetName("unknown card type!");
                SetDescription("unknown card type!");
                SetCoins(false);
            }
        }

        public void DeInit()
        {
            button?.onClick?.RemoveAllListeners();
        }

        private void SetPicture(int picture)
        {
            if (cardPicture != null)
                cardPicture.sprite = cardPictures.GetSpriteByIndex(picture);
        }

        private void SetCoins(bool showCoins, int coinsCount = 0)
        {
            if (!showCoins)
                cardCoins.SetActive(false);
            else
            {
                cardCoins.SetActive(true);
                if (cardCoinsCount != null)
                    cardCoinsCount.text = coinsCount.ToString();
            }
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

        private void SubsribeToButton(bool isInteractable)
        {
            if (isInteractable)
            {
                pointerHandler.enabled = true;
                pointerHandler?.Init(OnPointerEnter, OnPointerExit, OnPointerDown);
            }
            else
            {
                pointerHandler.enabled = false;
            }
        }

        private void OnPointerEnter()
        {
            animator?.SetTrigger("Highlighted");
        }

        private void OnPointerExit()
        {
            animator?.SetTrigger("Normal");
        }

        private void OnPointerDown()
        {
            if (!_hasBeenClicked)
            {
                _hasBeenClicked = true;
                Debug.Log(_cardType.Metadata.Name);
                _onCardCasted?.Invoke(_cardData.CardId);
            }
        }
    }
}
