using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solcery.FSM.Game
{
    public class GameState : State
    {
        [SerializeField] private string _sceneName;

        public override async UniTask Enter()
        {
            await base.Enter();

            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in GameState");
                return;
            }

            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override async UniTask Exit()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in GameState");
                return;
            }

            await SceneManager.UnloadSceneAsync(_sceneName);
            await base.Exit();
        }
    }
}
