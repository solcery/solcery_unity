using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Menu
{
    public class UIMenu : Singleton<UIMenu>
    {
        public UIMenuButtonTooltips Tooltips => tooltips;

        [SerializeField] private UIMenuButtonTooltips tooltips = null;


        public void Init()
        {

        }

        public void DeInit()
        {

        }
    }
}
