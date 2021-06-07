using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Grimmz.FSM.Game
{
    [CreateAssetMenu(menuName = "Grimmz/FSM/Game/States/Farm", fileName = "Farm")]
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
