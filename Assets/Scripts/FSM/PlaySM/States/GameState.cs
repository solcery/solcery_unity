using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/Game", fileName = "Game")]
    public class GameState : PlayState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}
