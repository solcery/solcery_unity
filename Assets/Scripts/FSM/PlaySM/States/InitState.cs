using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/Init", fileName = "Init")]
    public class InitState : PlayState
    {
        [SerializeField] private PlayTrigger openLobby = null;
        [SerializeField] private PlayTrigger openGame = null;

        public override async UniTask Enter()
        {
            await base.Enter();

            var playerGameStatus = PlayerGameStatusTracker.Instance?.PlayerStatus.Value;

            if (playerGameStatus == PlayerGameStatus.NotInGame || playerGameStatus == PlayerGameStatus.WaitingForOpponent)
            {
                openLobby?.Activate();
            }
            else if (playerGameStatus == PlayerGameStatus.InGame)
            {
                openGame?.Activate();
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}
