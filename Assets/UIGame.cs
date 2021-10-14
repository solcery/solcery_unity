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

        public void OnGameContentUpdate(GameContent gameContent)
        {
            // Debug.Log("UIGame.OnGameContentUpdate");
            board?.OnGameContentUpdate(gameContent);
        }

        public void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            // Debug.Log("UIGame.OnGameDisplayUpdate");
            board?.OnGameDisplayUpdate(gameDisplay);
        }

        public void OnGameStateDiffUpdate(GameState gameState)
        {
            // Debug.Log("UIGame.OnGameStateUpdate");
            board?.OnGameStateDiffUpdate(gameState);
        }
    }
}
