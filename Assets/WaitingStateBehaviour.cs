using Cysharp.Threading.Tasks;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class WaitingStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            Reactives.SubscribeWithoutCurrent(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.SubscribeWithoutCurrent(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.SubscribeWithoutCurrent(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            await base.OnExitState();
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            UIWaiting.Instance?.GameContentWaitingElement?.SetWaiting(gameContent == null);
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            UIWaiting.Instance?.GameDisplayWaitingElement?.SetWaiting(gameDisplay == null);
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            UIWaiting.Instance?.GameStateWaitingElement?.SetWaiting(gameState == null);
        }
    }
}
