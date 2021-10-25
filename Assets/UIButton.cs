using System.Collections.Generic;
using Solcery.React;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIButton : MonoBehaviour, IBoardPlace
    {
        public PlaceDisplayData DisplayData { get => _displayData; set => _displayData = value; }
        public bool AreCardsFaceDown => _areCardsFaceDown;

        [SerializeField] private Animator animator = null;
        [SerializeField] private Button button = null;
        [SerializeField] private TextMeshProUGUI buttonText = null;
        [SerializeField] private GameObject outline = null;
        [SerializeField] protected Image bgImage = null;


        private PlaceDisplayData _displayData;
        protected bool _areCardsFaceDown;

        private List<CardData> _cards;
        private CardData _topCardData;
        private CardType _topCardType;

        public Vector3 GetCardDestination(int cardId)
        {
            return Vector3.one;
        }

        public Vector3 GetCardRotation(int cardId)
        {
            return Vector3.zero;
        }

        public Vector2 GetCardSize(int cardId)
        {
            return Vector2.one;
        }

        public Transform GetCardsParent()
        {
            return this.transform;
        }

        public void OnCardArrival(int cardId)
        {

        }

        public void UpdateGameContent(GameContent gameContent)
        {
            UpdateWithCards(_displayData, gameContent, _cards);
        }

        public void UpdateWithCards(PlaceDisplayData displayData, GameContent gameContent, List<CardData> cards)
        {
            _displayData = displayData;
            button.onClick.RemoveAllListeners();

            _cards = cards;

            SetBgColor();

            if (_cards == null || _cards.Count <= 0)
            {
                if (buttonText != null)
                    buttonText.text = "No cards in this place";

                return;
            }

            _topCardData = _cards[0];

            if (_topCardData == null)
            {
                if (buttonText != null)
                    buttonText.text = "Top card is null";

                return;
            }

            SetShaking(false);
            button.onClick.AddListener(() =>
            {
                SetShaking(true);
                UnityToReact.Instance?.CallCastCard(_topCardData.CardId);
            });

            _topCardType = gameContent.GetCardTypeById(_topCardData.CardType);

            if (_topCardType == null)
            {
                if (buttonText != null)
                    buttonText.text = "CardType is null";

                return;
            }

            if (buttonText != null)
                buttonText.text = _topCardType.Metadata.Name;

            CheckIfHighlighted();
        }

        private void SetBgColor()
        {
            if (_displayData.HasBg)
                if (ColorUtility.TryParseHtmlString(_displayData.BgColor, out var bgColor))
                    if (bgImage != null)
                    {
                        bgImage.gameObject.SetActive(true);
                        bgImage.color = bgColor;
                        return;
                    }

            if (bgImage != null)
                bgImage.gameObject.SetActive(false);
        }

        private void CheckIfHighlighted()
        {
            var highlighted = false;
            if (_topCardData.TryGetAttrValue("highlighted", out var highlightInt))
                if (highlightInt == 1)
                    highlighted = true;

            SetHighlighted(highlighted);
        }

        private void SetHighlighted(bool isHighlighted)
        {
            if (outline != null)
                outline?.SetActive(isHighlighted);
        }

        private void SetShaking(bool isShaking)
        {
            if (animator != null)
                animator.SetBool("IsShaking", isShaking);
        }
    }
}
