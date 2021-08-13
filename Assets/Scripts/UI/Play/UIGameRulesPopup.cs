using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public class UIGameRulesPopup : Singleton<UIGameRulesPopup>
    {
        [SerializeField] Canvas canvas = null;
        [SerializeField] private Button closeButton = null;

        public void Open()
        {
            if (canvas != null) canvas.enabled = true;
            closeButton?.onClick?.AddListener(Close);
        }

        private void Close()
        {
            if (canvas != null) canvas.enabled = false;
        }
    }
}
