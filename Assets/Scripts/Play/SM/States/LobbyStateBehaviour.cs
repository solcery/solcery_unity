using Cysharp.Threading.Tasks;
using Solcery.Modules;
using Solcery.UI.Play.Lobby;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class LobbyStateBehaviour : PlayStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            UILobby.Instance?.Init();
            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            UILobby.Instance?.DeInit();

            await base.OnExitState();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                UILobby.Instance?.NotInGame();
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                UILobby.Instance?.WaitingForOpponent();
            }
            else
            {
                stateMachine.Trigger("LobbyToGame");
            }
        }
    }
}
