using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Game
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Game/States/Create", fileName = "Create")]
    public class CreateState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            await Solcery.Create.Instance.Init();
        }

        public override async UniTask Exit()
        {
            Solcery.Create.Instance?.DeInit();
            await base.Exit();
        }
    }
}
