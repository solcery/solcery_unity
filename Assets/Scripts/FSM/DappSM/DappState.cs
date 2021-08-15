using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solcery.FSM.Dapp
{
    public abstract class DappState : State<DappState, DappTrigger, DappTransition>
    {
        [SerializeField] private string _sceneName;

        public override async UniTask Enter(Action<DappTransition> performTransition)
        {
            await base.Enter(performTransition);

            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in DappState");
                return;
            }

            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Empty scene name in DappState");
                return;
            }

            await SceneManager.UnloadSceneAsync(_sceneName);
        }
    }
}
