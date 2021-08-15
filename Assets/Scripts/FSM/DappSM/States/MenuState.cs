using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Dapp
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Dapp/States/Menu", fileName = "Menu")]
    public class MenuState : DappState
    {
        public override async UniTask Enter(Action<DappTransition> performTransition)
        {
            await base.Enter(performTransition);
            Menu.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Menu.Instance?.DeInit();
            await base.Exit();
        }
    }
}
