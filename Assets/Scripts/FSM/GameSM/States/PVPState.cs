using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Game
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Game/States/PVP", fileName = "PVP")]
    public class PVPState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            PVP.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            PVP.Instance?.DeInit();
            await base.Exit();
        }
    }
}
