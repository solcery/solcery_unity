using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solcery.FSM.Play
{
    public abstract class PlayState : State<PlayState, PlayTrigger, PlayTransition>
    {
        [SerializeField] private bool hasScene;
        [ShowIf("hasScene")] [SerializeField] private string sceneName;

        public override async UniTask Enter()
        {
            if (hasScene)
            {
                if (string.IsNullOrEmpty(sceneName))
                {
                    Debug.LogError("Empty scene name in PlayState");
                }
                else
                {
                    await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                }
            }
        }

        public override async UniTask Exit()
        {
            if (hasScene)
            {
                if (string.IsNullOrEmpty(sceneName))
                {
                    Debug.LogError("Empty scene name in PlayState");
                }
                else
                {
                    await SceneManager.UnloadSceneAsync(sceneName);
                }
            }
        }
    }
}
