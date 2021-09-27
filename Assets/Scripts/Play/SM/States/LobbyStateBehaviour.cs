using Cysharp.Threading.Tasks;
using Solcery.Modules;
using Solcery.UI.Play.Lobby;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class LobbyStateBehaviour : PlayStateBehaviour
    {
        private GameContent _gameContent;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            UILobby.Instance?.Init();

            Reactives.Subscribe(Game.Instance?.GameContent, OnGameContentUpdate, _stateCTS.Token);
            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _stateCTS.Token);
        }

        protected override async UniTask OnExitState()
        {
            UILobby.Instance?.DeInit();

            await base.OnExitState();
        }

        private void OnGameContentUpdate(GameContent gameContent)
        {
            _gameContent = gameContent;
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null || _gameContent == null)
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
