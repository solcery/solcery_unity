using Cysharp.Threading.Tasks;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class WaitingStateBehaviour : GameStateBehaviour
    {
        GameContent _gameContent;
        GameDisplay _gameDisplay;
        GameState _gameState;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            UIWaiting.Instance?.Init();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameDisplay, OnGameDisplayUpdate, _stateCTS.Token);
            Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            UIWaiting.Instance?.DeInit();

            await base.OnExitState();
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            _gameContent = gameContent;
            UIWaiting.Instance?.GameContentWaitingElement?.SetWaiting(gameContent == null);
            CheckIfCanStartGame();
        }

        private void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            UnityEngine.Debug.Log("OnGameDisplayUpdate");
            _gameDisplay = gameDisplay;
            UIWaiting.Instance?.GameDisplayWaitingElement?.SetWaiting(gameDisplay == null);
            CheckIfCanStartGame();
        }

        private void OnGameStateUpdate(GameState gameState)
        {
            _gameState = gameState;
            UIWaiting.Instance?.GameStateWaitingElement?.SetWaiting(gameState == null);
            CheckIfCanStartGame();
        }

        private void CheckIfCanStartGame()
        {
            if (_gameContent == null) return;
            if (_gameDisplay == null) return;
            if (_gameState == null) return;

            stateMachine?.Trigger("Init");
        }
    }
}
