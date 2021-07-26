using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Game
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Dapp/States/Menu", fileName = "Menu")]
    public class MenuState : DappState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Menu.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Menu.Instance?.DeInit();
            await base.Exit();
        }
    }
}
