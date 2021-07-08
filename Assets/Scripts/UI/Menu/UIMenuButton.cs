using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Menu
{
    public class UIMenuButton : UIGameTransitionButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Color titleHighlightedColor;
        [SerializeField] private Color titleUnhighlightedColor;
        [SerializeField] [Multiline(8)] private string tooltip = null;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (title != null)
                title.color = titleHighlightedColor;

            UIMenu.Instance.Tooltips.SetText(tooltip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (title != null)
                title.color = titleUnhighlightedColor;

            UIMenu.Instance.Tooltips.Disable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}
