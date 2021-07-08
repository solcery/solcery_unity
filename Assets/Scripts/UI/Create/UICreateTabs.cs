using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UICreateTabs : MonoBehaviour
    {
        [SerializeField] private List<UICreateTab> tabs = null;

        private int _currentlySelected;

        public void Init()
        {
            InitTabs();
        }

        public void DeInit()
        {

        }

        private void InitTabs()
        {
            if (tabs != null && tabs.Count > 0)
                for (int i = 0; i < tabs.Count; i++)
                {
                    var initialState = i == _currentlySelected ? UICreateTabState.Selected : UICreateTabState.Unselected;
                    tabs[i].Init(i, initialState, OnTabClicked);
                }
        }

        public void OnTabClicked(int clickedTabIndex)
        {
            if (_currentlySelected != clickedTabIndex)
            {
                tabs[_currentlySelected].SetState(UICreateTabState.Unselected);
                tabs[clickedTabIndex].SetState(UICreateTabState.Selected);

                _currentlySelected = clickedTabIndex;
            }
        }
    }
}
