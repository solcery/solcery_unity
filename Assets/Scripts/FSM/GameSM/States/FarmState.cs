using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Game
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Game/States/Farm", fileName = "Farm")]
    public class FarmState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Farm.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Farm.Instance?.DeInit();
            await base.Exit();
        }
    }
}
