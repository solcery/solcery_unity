using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Menu
{
    public class UIMenu : Singleton<UIMenu>
    {
        public UIMenuButtonTooltips Tooltips => tooltips;

        [SerializeField] private UIMenuButtonTooltips tooltips = null;
        [SerializeField] private Button twitterButton = null;

        public void Init()
        {
            twitterButton.onClick.AddListener(() => UnityToReact.Instance?.CallOpenLinkInNewTab("https://twitter.com"));
        }

        public void DeInit()
        {
            twitterButton.onClick.RemoveAllListeners();
        }
    }
}
