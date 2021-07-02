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
            button.image.color = _state switch
            {
                UICreateTabState.Highlighted => highlightedColor,
                UICreateTabState.Selected => selectedColor,
                UICreateTabState.Unselected => unselectedColor,
                _ => unselectedColor
            };
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
