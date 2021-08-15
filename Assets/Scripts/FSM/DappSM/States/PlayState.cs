using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Dapp
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Dapp/States/Play", fileName = "Play")]
    public class PlayState : DappState
    {
        public override async UniTask Enter(Action<DappTransition> performTransition)
        {
            await base.Enter(performTransition);
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}
