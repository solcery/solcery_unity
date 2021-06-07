using Grimmz.Utils;
using UnityEngine;

namespace Grimmz.UI.Menu
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
