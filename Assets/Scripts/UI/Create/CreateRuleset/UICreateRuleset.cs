using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UICreateRuleset : Singleton<UICreateRuleset>
    {
        public UICardsLineup LineupUnderPointer { get; private set; }

        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private RectTransform content = null;
        [SerializeField] private UICardsLineup initialCardsLineup = null;

        public void Init()
        {
            initialCardsLineup?.Init(content, OnPointerEnterLineup, OnPointerExitLineup);
        }

        public void DeInit()
        {

        }

        public void Open()
        {
            canvas.enabled = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Close()
        {
            canvas.enabled = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OnPointerEnterLineup(UICardsLineup lineup)
        {
            LineupUnderPointer = lineup;
        }

        private void OnPointerExitLineup(UICardsLineup lineup)
        {
            LineupUnderPointer = null;
        }
    }
}
