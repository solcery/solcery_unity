using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI
{
    public class UIGame : Singleton<UIGame>
    {
        [SerializeField] Canvas canvas = null;
        [SerializeField] private UIBoard board = null;

        public void Init()
        {
            if (canvas != null) canvas.enabled = true;
            board?.Init();
        }

        public void DeInit()
        {
            board?.DeInit();
            if (canvas != null) canvas.enabled = false;
        }

        public void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            board?.OnGameDisplayUpdate(gameDisplay);
        }

        public void OnGameStateDiffUpdate(GameState gameState)
        {
            board?.OnGameStateDiffUpdate(gameState);
        }
    }
}
