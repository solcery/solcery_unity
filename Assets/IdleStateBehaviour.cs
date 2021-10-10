using Cysharp.Threading.Tasks;
using Solcery.UI;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery
{
    public class IdleStateBehaviour : GameStateBehaviour
    {
        private GameDisplay _lastGameDisplay;
        private GameState _lastGameState;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
        }

        private void OnGameAllUpdate(GameAll gameAll)
        {

        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            if (gameContent == null) { ExitGame(); return; }
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            if (gameDisplay == null) { ExitGame(); return; }

            if (gameDisplay.HasBeenProcessed)
            {
                Debug.Log("GameDisplay has been processed");
                return;
            }

            _lastGameDisplay = gameDisplay;
            _lastGameDisplay.HasBeenProcessed = true;
            UIGame.Instance?.OnGameDisplayUpdate(gameDisplay);
            var gameStateWithDiff = GameStateDiffTracker.Instance?.GetGameStateDiff(null, _lastGameState);
            UIGame.Instance?.OnGameStateDiffUpdate(gameStateWithDiff);
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            if (gameState == null) { ExitGame(); return; }

            if (gameState.HasBeenProcessed)
            {
                Debug.Log("GameState has been processed");
                return;
            }

            var gameStateWithDiff = GameStateDiffTracker.Instance?.GetGameStateDiff(_lastGameState, gameState);

            if (gameStateWithDiff != null)
            {
                _lastGameState = gameStateWithDiff;
                _lastGameState.HasBeenProcessed = true;
                UIGame.Instance?.OnGameStateDiffUpdate(gameStateWithDiff);
                if (UICardAnimator.Instance.HasSomethingToAnimate)
                    stateMachine?.Trigger("Animate");
            }
        }

        private void ExitGame()
        {
            stateMachine?.Trigger("ExitGame");
        }
    }
}
