using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Game
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Game/States/Play", fileName = "Play")]
    public class PlayState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Play.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Play.Instance?.DeInit();
            await base.Exit();
        }
    }
}