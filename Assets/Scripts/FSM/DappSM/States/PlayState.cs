using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Dapp
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Dapp/States/Play", fileName = "Play")]
    public class PlayState : DappState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            // Solcery.Play.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            // Solcery.Play.Instance?.DeInit();
            await base.Exit();
        }
    }
}
