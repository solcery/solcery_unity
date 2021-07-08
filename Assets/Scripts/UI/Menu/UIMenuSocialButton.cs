using Solcery.WebGL;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI.Menu
{
    public class UIMenuSocialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string link = null;
        [SerializeField] private Button button = null;

        [SerializeField] private Image frameImage = null;
        [SerializeField] private Image logoImage = null;

        [SerializeField] private Color frameNormalColor;
        [SerializeField] private Color frameHighlightedColor;

        [SerializeField] private Color logoNormalColor;
        [SerializeField] private Color logoHighlightedColor;

        public void Init()
        {
            if (button != null)
            {
                button.interactable = true;
                button.onClick.AddListener(OnButtonClicked);
            }

            ChangeFrameColor(frameNormalColor);
            ChangeLogoColor(logoNormalColor);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeFrameColor(frameHighlightedColor);
            ChangeLogoColor(logoHighlightedColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeFrameColor(frameNormalColor);
            ChangeLogoColor(logoNormalColor);
        }

        private void OnButtonClicked()
        {
            if (!string.IsNullOrEmpty(link))
                UnityToReact.Instance?.CallOpenLinkInNewTab(link);
        }

        private void ChangeFrameColor(Color newColor)
        {
            if (frameImage != null) frameImage.color = newColor;
        }

        private void ChangeLogoColor(Color newColor)
        {
            if (logoImage != null) logoImage.color = newColor;
        }
    }
}
