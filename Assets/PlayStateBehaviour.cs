using Cysharp.Threading.Tasks;
using Ilumisoft.VisualStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStateBehaviour : StateBehaviour
{
    public override string StateID => stateId;
    [SerializeField] private string stateId;

    [SerializeField] private bool hasScene;
    [ShowIf("hasScene")] [SerializeField] private string sceneName;

    protected override async UniTask OnEnterState()
    {
        await base.OnEnterState();

        if (hasScene)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Empty scene name in PlayState");
            }
            else
            {
                await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).WithCancellation(_stateCTS.Token);
            }
        }
    }

    protected override async UniTask OnExitState()
    {
        if (hasScene)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Empty scene name in PlayState");
            }
            else if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(sceneName);
            }
        }

        await base.OnExitState();
    }
}
