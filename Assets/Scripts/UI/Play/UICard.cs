using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UICard : MonoBehaviour
    {
        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private TextMeshProUGUI cardName = null;
        [SerializeField] private TextMeshProUGUI cardDescription = null;

        public void Init(CardData cardData, bool isInteractable, Action<string, int> onCardCasted = null)
        {
            cardPicture.sprite = cardPictures.GetSpriteByIndex(cardData.Metadata.Picture);
            cardName.text = cardData.Metadata.Name;
            cardDescription.text = cardData.Metadata.Description;

            if (button != null)
            {
                button.interactable = isInteractable;
                if (isInteractable)
                    button.onClick.AddListener(() => { onCardCasted?.Invoke(cardData.MintAddress, cardData.CardId); });
            }
        }

        public void DeInit()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}

