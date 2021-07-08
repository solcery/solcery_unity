using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Create
{
    public class UICreateTab : UICreateTransitionButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color highlightedColor;
        [SerializeField] private Color unselectedColor;

        private UICreateTabState _state;
        private int _tabIndex;
        private Action<int> _onTabClicked;

        public void Init(int tabIndex, UICreateTabState initialState, Action<int> onTabClicked)
        {
            _tabIndex = tabIndex;
            _state = initialState;
            _onTabClicked = onTabClicked;

            ApplyState();
        }

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();
            _onTabClicked?.Invoke(_tabIndex);
        }

        public void SetState(UICreateTabState state)
        {
            _state = state;
            ApplyState();
        }

        private void ApplyState()
        {
            switch (_state)
            {
                case UICreateTabState.Highlighted:
                    button.image.color = highlightedColor;
                    break;
                case UICreateTabState.Selected:
                    button.image.color = selectedColor;
                    button.interactable = false;
                    break;
                case UICreateTabState.Unselected:
                    button.image.color = unselectedColor;
                    button.interactable = true;
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_state != UICreateTabState.Selected)
                SetState(UICreateTabState.Highlighted);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_state != UICreateTabState.Selected)
                SetState(UICreateTabState.Unselected);
        }
    }
}
