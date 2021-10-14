using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICollectionCard : MonoBehaviour, IPointerDownHandler
    {
        public CollectionCardType CardType => _cardType;

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private LayoutElement le = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        [HideInInspector] [SerializeField] private CollectionCardType _cardType;
        private int _indexInCollection;
        private Action<int> _onClick, _onPointerDown;

        public void Init(CollectionCardType cardType, int indexInCollection, Action<int> onClick, Action<int> onPointerDown)
        {
            _cardType = cardType;
            _indexInCollection = indexInCollection;
            _onClick = onClick;
            _onPointerDown = onPointerDown;

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoinsCount(0);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SetButton();
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

        public void DetatchFromGroup()
        {
            le.ignoreLayout = true;
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

        private void SetButton()
        {
            button?.onClick.AddListener(() => _onClick?.Invoke(_indexInCollection));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _onPointerDown?.Invoke(_indexInCollection);
        }
    }
}
