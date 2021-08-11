using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solcery.FSM.Play
{
    public abstract class PlayState : State<PlayState, PlayTrigger, PlayTransition>
    {
        [SerializeField] private string _sceneName;

        public override async UniTask Enter()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in PlayState");
                return;
            }

            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override async UniTask Exit()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in PlayState");
                return;
            }

            await SceneManager.UnloadSceneAsync(_sceneName);
        }
    }
}
