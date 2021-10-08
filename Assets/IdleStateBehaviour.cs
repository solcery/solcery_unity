using Cysharp.Threading.Tasks;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class IdleStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            if (gameContent == null) ExitGame();
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            if (gameDisplay == null) ExitGame();
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            if (gameState == null) ExitGame();
        }

        private void ExitGame()
        {
            stateMachine?.Trigger("ExitGame");
        }
    }
}
