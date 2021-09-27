using Cysharp.Threading.Tasks;
using Solcery.UI.Play.Game;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class GameStateBehaviour : PlayStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            BoardDataDiffTracker.Instance?.Init();
            GameResultTracker.Instance?.Init();
            UIGame.Instance?.Init();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(BoardDataDiffTracker.Instance?.BoardDataWithDiff, OnBoardUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            BoardDataDiffTracker.Instance?.DeInit();
            GameResultTracker.Instance?.DeInit();
            UIGame.Instance?.DeInit();

            await base.OnExitState();
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            UIGame.Instance?.OnGameContentUpdate(gameContent);
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
                stateMachine.Trigger("GameToLobby");
            else if (boardData.Players != null && boardData.Players.Count < 2)
                stateMachine.Trigger("GameToLobby");
            else
                UIGame.Instance?.OnBoardUpdate(boardData);
        }
    }
}
