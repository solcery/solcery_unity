using Cysharp.Threading.Tasks;
using Solcery.UI;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery
{
    public class IdleStateBehaviour : GameStateBehaviour
    {
        private GameContent _lastGameContent;
        private GameDisplay _lastGameDisplay;
        private GameState _lastGameState;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            if (_lastGameContent != null)
                _lastGameContent.HasBeenProcessed = false;
            if (_lastGameDisplay != null)
                _lastGameDisplay.HasBeenProcessed = false;
            if (_lastGameState != null)
                _lastGameState.HasBeenProcessed = false;

            _lastGameContent = null;
            _lastGameDisplay = null;
            _lastGameState = null;

            await base.OnExitState();
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            if (gameContent == null) { Debug.Log("null GameContent"); ExitGame(); return; }

            if (gameContent.HasBeenProcessed)
            {
                // Debug.Log("GameContent has been processed");
                return;
            }

            _lastGameContent = gameContent;
            _lastGameContent.HasBeenProcessed = true;

            UIGame.Instance?.OnGameContentUpdate(gameContent);
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            if (gameDisplay == null) { Debug.Log("null GameDisplay"); ExitGame(); return; }

            if (gameDisplay.HasBeenProcessed)
            {
                // Debug.Log("GameDisplay has been processed");
                return;
            }

            _lastGameDisplay = gameDisplay;
            _lastGameDisplay.HasBeenProcessed = true;
            UIGame.Instance?.OnGameDisplayUpdate(gameDisplay);
            var gameStateWithDiff = GameStateDiffTracker.Instance?.GetGameStateDiff(null, _lastGameState);

            if (gameStateWithDiff != null)
            {
                UIGame.Instance?.OnGameStateDiffUpdate(gameStateWithDiff);
                if (UICardAnimator.Instance.HasSomethingToAnimate)
                    stateMachine?.Trigger("Animate");
            }
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            if (gameState == null) { Debug.Log("null GameState"); ExitGame(); return; }

            if (gameState.HasBeenProcessed)
            {
                // Debug.Log("GameState has been processed");
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
