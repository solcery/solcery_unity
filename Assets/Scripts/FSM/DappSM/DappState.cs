using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solcery.FSM.Dapp
{
    public abstract class DappState : State
    {
        [SerializeField] private string _sceneName;

        public override async UniTask Enter()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in DappState");
                return;
            }

            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override async UniTask Exit()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in DappState");
                return;
            }

            await SceneManager.UnloadSceneAsync(_sceneName);
        }
    }
}
