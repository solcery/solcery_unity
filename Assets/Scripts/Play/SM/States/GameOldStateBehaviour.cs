using Cysharp.Threading.Tasks;
using Solcery.UI.Play.Game;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class GameOldStateBehaviour : PlayStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            GameStateDiffTracker.Instance?.Init();
            GameResultTracker.Instance?.Init();
            OldUIGame.Instance?.Init();

            Reactives.Subscribe(OldGame.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(GameStateDiffTracker.Instance?.BoardDataWithDiff, OnBoardUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            GameStateDiffTracker.Instance?.DeInit();
            GameResultTracker.Instance?.DeInit();
            OldUIGame.Instance?.DeInit();

            await base.OnExitState();
        }

        private void OnGameContentUpdate(OldGameContent gameContent)
        {
            OldUIGame.Instance?.OnGameContentUpdate(gameContent);
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
                stateMachine.Trigger("GameToLobby");
            else if (boardData.Players != null && boardData.Players.Count < 2)
                stateMachine.Trigger("GameToLobby");
            else
                OldUIGame.Instance?.OnBoardUpdate(boardData);
        }
    }
}
