using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI
{
    public class UIGame : Singleton<UIGame>
    {
        [SerializeField] Canvas canvas = null;

        public void Init()
        {
            if (canvas != null) canvas.enabled = true;
            UIBoard.Instance?.Init();
        }

        public void DeInit()
        {

            UIBoard.Instance?.DeInit();
            if (canvas != null) canvas.enabled = false;
        }
    }
}
