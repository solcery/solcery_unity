using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

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

            var combined = Game.Instance?.GameContent.CombineLatest(Game.Instance?.GameDisplay, Game.Instance?.GameState, (x, y, z) => (x, y, z)).ToReadOnlyAsyncReactiveProperty(_stateCTS.Token);
            combined?.ForEachAsync(p => OnUpdate(p.x, p.y, p.z), _stateCTS.Token);
        }

        private void OnUpdate(GameContent gameContent, GameDisplay gameDisplay, GameState gameState)
        {
            UIWaiting.Instance?.GameContentWaitingElement?.SetWaiting(gameContent == null);
            UIWaiting.Instance?.GameDisplayWaitingElement?.SetWaiting(gameDisplay == null);
            UIWaiting.Instance?.GameStateWaitingElement?.SetWaiting(gameState == null);

            if (gameContent == null) return;
            if (gameDisplay == null) return;
            if (gameState == null) return;

            stateMachine?.Trigger("Init");
        }
    }
}
