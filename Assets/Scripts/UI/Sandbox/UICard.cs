using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grimmz.UI
{
    public class UICard : MonoBehaviour
    {
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;

        public void Init(CardData cardData, Action<string> onCardCasted)
        {
            cardPicture.sprite = cardPictures.GetSpriteByIndex(cardData.Metadata.Picture);
            cardName.text = cardData.Metadata.Name;
            cardDescription.text = cardData.Metadata.Description;

            button.onClick.AddListener(() => { onCardCasted?.Invoke(cardData.MintAdress); });
        }

        public void DeInit()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}

