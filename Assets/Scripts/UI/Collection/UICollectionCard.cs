using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICollectionCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private LayoutElement le = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        private CollectionCardType _cardType;
        private int _indexInCollection;
        private Action<int> _onClick, _onPointerDown, _onPointerUp;

        public void Init(CollectionCardType cardType, int indexInCollection, Action<int> onClick, Action<int> onPointerDown, Action<int> onPointerUp)
        {
            _cardType = cardType;
            _indexInCollection = indexInCollection;
            _onClick = onClick;
            _onPointerDown = onPointerDown;
            _onPointerUp = onPointerUp;

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoinsCount(_cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
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

        public void OnPointerDown(PointerEventData eventData)
        {
            _onPointerDown?.Invoke(_indexInCollection);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _onPointerUp?.Invoke(_indexInCollection);
        }
    }
}
