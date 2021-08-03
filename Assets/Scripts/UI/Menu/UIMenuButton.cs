using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Menu
{
    public class UIMenuButton : UIDappTransitionButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool highlighTitle;
        [ShowIf("highlighTitle")] [SerializeField] private TextMeshProUGUI title;
        [ShowIf("highlighTitle")] [SerializeField] private Color titleHighlightedColor;
        [ShowIf("highlighTitle")] [SerializeField] private Color titleUnhighlightedColor;
        [SerializeField] [Multiline(5)] private string tooltip = null;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (highlighTitle && title != null)
                title.color = titleHighlightedColor;

            UIMenu.Instance.Tooltips.SetText(tooltip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (highlighTitle && title != null)
                title.color = titleUnhighlightedColor;

            UIMenu.Instance.Tooltips.Disable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}
