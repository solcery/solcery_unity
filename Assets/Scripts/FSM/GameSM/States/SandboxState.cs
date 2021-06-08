using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Grimmz.FSM.Game
{
    [CreateAssetMenu(menuName = "Grimmz/FSM/Game/States/Sandbox", fileName = "Sandbox")]
    public class SandboxState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Sandbox.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Sandbox.Instance?.DeInit();
            await base.Exit();
        }
    }
}
