using Cysharp.Threading.Tasks;
using Solcery.UI;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery
{
    public class IdleStateBehaviour : GameStateBehaviour
    {
        private int _lastStatesProcessed;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
            Reactives.Subscribe(GameStateDiffTracker.Instance?.GameStateWithDiff, OnGameStateDiffUpdate, _stateCTS.Token);
        }

        private void OnGameStateDiffUpdate(GameState gameState)
        {
            Debug.Log($"states processed : {GameStateDiffTracker.Instance.StatesProcessed}");
            Debug.Log($"lastStatesProcessed : {_lastStatesProcessed}");
            if (GameStateDiffTracker.Instance.StatesProcessed == _lastStatesProcessed)
            {
                UnityEngine.Debug.Log($"already processed state #: {_lastStatesProcessed}");
                return;
            }

            _lastStatesProcessed = GameStateDiffTracker.Instance.StatesProcessed;

            UnityEngine.Debug.Log("OnGameStateDiffUpdate");
            if (gameState == null) { ExitGame(); return; }
            UIGame.Instance?.OnGameStateUpdate(gameState);

            if (UICardAnimator.Instance.HasSomethingToAnimate)
                stateMachine?.Trigger("Animate");
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            if (gameContent == null) { ExitGame(); return; }
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            if (gameDisplay == null) { ExitGame(); return; }
            UIGame.Instance?.OnGameDisplayUpdate(gameDisplay);
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            if (gameState == null) { ExitGame(); return; }
        }

        private void ExitGame()
        {
            stateMachine?.Trigger("ExitGame");
        }
    }
}
