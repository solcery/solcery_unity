using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Menu
{
    public class UIMenuSocialButtons : MonoBehaviour
    {
        [SerializeField] private List<UIMenuSocialButton> buttons = null;

        public void Init()
        {
            foreach (var button in buttons)
            {
                button.Init();
            }
        }

        public void DeInit()
        {
            
        }
    }
}
