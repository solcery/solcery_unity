using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class UIWaiting : Singleton<UIWaiting>
    {
        public UIWaitingElement GameContentWaitingElement => gameContentWaitingElement;
        public UIWaitingElement GameDisplayWaitingElement => gameDisplayWaitingElement;
        public UIWaitingElement GameStateWaitingElement => gameStateWaitingElement;

        [SerializeField] Canvas canvas = null;
        [SerializeField] UIWaitingElement gameContentWaitingElement = null;
        [SerializeField] UIWaitingElement gameDisplayWaitingElement = null;
        [SerializeField] UIWaitingElement gameStateWaitingElement = null;

        public void Init()
        {
            if (canvas != null) canvas.enabled = true;
        }

        public void DeInit()
        {
            if (canvas != null) canvas.enabled = false;
        }
    }
}
