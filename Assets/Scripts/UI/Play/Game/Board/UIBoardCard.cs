using System;
using Solcery.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIBoardCard : MonoBehaviour
    {
        public CardData CardData => _cardData;
        public bool IsFaceDown => _isFaceDown;
        public AspectRatioFitter ARF => arf;

        [SerializeField] private AspectRatioFitter arf = null;
        [SerializeField] private CanvasGroup cg = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private UIBoardCardPointerHandler pointerHandler = null;
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardImage = null;
        [SerializeField] private Image cardFrameFaceUp = null;
        [SerializeField] private Image cardFrameFaceDown = null;
        [SerializeField] private Image cardCoinsBackground = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;
        [SerializeField] private GameObject cardCoins = null;

        private CardData _cardData;
        private CardType _cardType;
        private Action<int> _onCardCasted;

        private bool _isFaceDown;
        private bool _isInteractable;
        private bool _showCoins;
        private bool _pointerDown = false;
        private bool _isPointerOver = false;

        public void UpdateGameContent(GameContent gameContent)
        {
            _cardType = gameContent.GetCardTypeById(_cardData.CardType);

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata);
                SetCoins(_showCoins, _cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SubsribeToButton();
            }
            else
            {
                Debug.LogWarning("CardType from this CardData doesn't exist in this GameState");

                SetName("unknown card type!");
                SetDescription("unknown card type!");
                SetCoins(false);
            }
        }

        public void Init(GameContent gameContent, CardData cardData, bool isFaceDown, bool isInteractable, bool showCoins = false, Action<int> onCardCasted = null)
        {
            _showCoins = showCoins;
            _isFaceDown = isFaceDown;
            _onCardCasted = onCardCasted;
            _cardData = cardData;

            SetSprite(null);
            SetInteractabe(isInteractable);
            SetAnimator();
            UpdateGameContent(gameContent);
        }

        public void SetFaceDown(bool isFaceDown)
        {
            _isFaceDown = isFaceDown;
            SetAnimator();
        }

        public void SetInteractabe(bool isInteractable)
        {
            _isInteractable = isInteractable;
            _pointerDown = false;
            if (pointerHandler != null)
                pointerHandler.enabled = true;

            if (animator != null)
            {
                if (!_isInteractable)
                {
                    if (animator != null)
                        animator?.SetTrigger("Idle");
                }
                else
                {
                    if (_isPointerOver)
                    {
                        if (animator != null)
                            animator?.SetTrigger("Highlighted");
                    }
                    else
                    {
                        if (animator != null)
                            animator?.SetTrigger("Idle");
                    }
                }
            }
        }

        public void StopShaking()
        {
            if (animator != null)
                animator?.SetBool("IsPressed", false);
        }

        public void SetAnimator()
        {
            if (animator != null)
                animator?.SetBool("IsFaceDown", _isFaceDown);
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
            if (cardImage != null) cardImage.maskable = false;
            if (cardFrameFaceUp != null) cardFrameFaceUp.maskable = false;
            if (cardCoinsBackground != null) cardCoinsBackground.maskable = false;
            if (cardName != null) cardName.maskable = false;
            if (cardDescription != null) cardDescription.maskable = false;
            if (cardCoinsCount != null) cardCoinsCount.maskable = false;

            if (cardFrameFaceDown != null) cardFrameFaceDown.maskable = false;
        }

        public void PlayTurningAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger(_isFaceDown ? "TurnFaceUp" : "TurnFaceDown");
            }
        }

        private void SetPicture(CardMetadata metadata)
        {
            if (cardImage == null)
                return;

            if (!string.IsNullOrEmpty(metadata.PictureUrl))
            {
                CardPicturesFromUrl.Instance?.GetTextureByUrl(metadata.PictureUrl, (sprite) => SetSprite(sprite));
            }
            else
                SetSprite(cardPictures.GetSpriteByIndex(metadata.Picture));
        }

        private void SetSprite(Sprite sprite)
        {
            if (cardImage == null)
                return;

            cardImage.sprite = sprite;
        }

        private void SetCoins(bool showCoins, int coinsCount = 0)
        {
            if (cardCoins == null || cardCoinsCount == null)
                return;

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
            pointerHandler?.Init(OnPointerEnter, OnPointerExit, OnPointerDown, OnPointerUp, OnDrag);
        }

        private void OnPointerEnter()
        {
            _isPointerOver = true;

            if (!_isInteractable)
                return;

            animator?.SetTrigger("Highlighted");
        }

        private void OnPointerExit()
        {
            _isPointerOver = false;

            if (!_isInteractable)
                return;

            animator?.SetTrigger("Idle");
            _pointerDown = false;
        }

        private void OnPointerDown()
        {
            if (!_isInteractable)
                return;

            if (!_pointerDown)
            {
                _pointerDown = true;
            }
        }

        private void OnPointerUp()
        {
            if (!_isInteractable)
                return;

            if (_pointerDown)
            {
                pointerHandler.enabled = false;
                animator?.SetBool("IsPressed", true);
                _isPointerOver = false;
                _onCardCasted?.Invoke(_cardData.CardId);
            }
        }

        private void OnDrag()
        {
            if (!_isInteractable)
                return;
        }
    }
}
