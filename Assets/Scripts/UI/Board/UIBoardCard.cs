using System;
using Solcery.Modules.Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIBoardCard : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cg = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private UIBoardCardPointerHandler pointerHandler = null;
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private Image cardFrame = null;
        [SerializeField] private Image cardCoinsBackground = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;
        [SerializeField] private GameObject cardCoins = null;

        private BoardCardData _cardData;
        private BoardCardType _cardType;
        private Action<int> _onCardCasted;

        private bool _isInteractable;
        private bool _hasBeenClicked = false;

        public void Init(BoardCardData cardData, bool isInteractable, bool showCoins = false, Action<int> onCardCasted = null)
        {
            _isInteractable = isInteractable;

            _cardData = cardData;
            _cardType = Board.Instance.BoardData.Value.GetCardTypeById(_cardData.CardType);
            _onCardCasted = onCardCasted;

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoins(showCoins, _cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SubsribeToButton();
            }
            else
            {
                Debug.LogError("BoardCardType from this BoardCardData doesn't exist in this BoardData");

                SetName("unknown card type!");
                SetDescription("unknown card type!");
                SetCoins(false);
            }
        }

        public void DeInit()
        {

        }

        public void SetVisibility(bool isVisible)
        {
            if (cg != null)
            {
                cg.alpha = isVisible ? 1f : 0;
                cg.interactable = isVisible ? true : false;
                cg.blocksRaycasts = isVisible ? true : false;
            }
        }

        public void MakeUnmaskable()
        {
            if (cardPicture != null) cardPicture.maskable = false;
            if (cardFrame != null) cardFrame.maskable = false;
            if (cardCoinsBackground != null) cardCoinsBackground.maskable = false;
            if (cardName != null) cardName.maskable = false;
            if (cardDescription != null) cardDescription.maskable = false;
            if (cardCoinsCount != null) cardCoinsCount.maskable = false;
        }

        public void PlayTurningAnimation()
        {
            if (animator != null)
                animator.SetTrigger("Turn");
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

        private void SubsribeToButton()
        {
            if (_isInteractable)
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
                animator?.SetTrigger("Pressed");
                pointerHandler.enabled = false;
                _onCardCasted?.Invoke(_cardData.CardId);
            }
        }
    }
}
