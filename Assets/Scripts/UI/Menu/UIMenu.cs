using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Menu
{
    public class UIMenu : Singleton<UIMenu>
    {
        public UIMenuButtonTooltips Tooltips => tooltips;

        [SerializeField] private UIMenuSocialButtons socialButtons = null;
        [SerializeField] private UIMenuButtonTooltips tooltips = null;

        public void Init()
        {
            socialButtons?.Init();
        }

        public void DeInit()
        {
            socialButtons?.DeInit();
        }
    }
}
