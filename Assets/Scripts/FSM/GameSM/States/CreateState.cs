using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Grimmz.FSM.Game
{
    [CreateAssetMenu(menuName = "Grimmz/FSM/Game/States/Create", fileName = "Create")]
    public class CreateState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Grimmz.Create.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Grimmz.Create.Instance?.DeInit();
            await base.Exit();
        }
    }
}
