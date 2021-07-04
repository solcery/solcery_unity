using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICollectionCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;
        [SerializeField] private TextMeshProUGUI cardCoinsCount = null;

        private CollectionCardType _cardType;

        public void Init(CollectionCardType cardType)
        {
            _cardType = cardType;

            if (_cardType != null)
            {
                SetPicture(_cardType.Metadata.Picture);
                SetCoinsCount(_cardType.Metadata.Coins);
                SetName(_cardType.Metadata.Name);
                SetDescription(_cardType.Metadata.Description);
                SubsribeToButton();
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

        private void SubsribeToButton()
        {
            button?.onClick.AddListener(OnCardClicked);
        }

        private void OnCardClicked()
        {
            Debug.Log($"clicked on {_cardType.Metadata.Name}");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"pointer enter {_cardType.Metadata.Name}");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log($"pointer exit {_cardType.Metadata.Name}");
        }
    }
}
