using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;

namespace Solcery.FSM.Play
{
    public class PlayerGameStatusTracker : Singleton<PlayerGameStatusTracker>
    {
        public AsyncReactiveProperty<PlayerGameStatus> PlayerStatus => _playerStatus;
        private AsyncReactiveProperty<PlayerGameStatus> _playerStatus = new AsyncReactiveProperty<PlayerGameStatus>(PlayerGameStatus.NotInGame);

        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(BoardDataDiffTracker.Instance?.BoardDataWithDiff, OnBoardUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                _playerStatus.Value = PlayerGameStatus.NotInGame;
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                _playerStatus.Value = PlayerGameStatus.WaitingForOpponent;
            }
            else
            {
                _playerStatus.Value = PlayerGameStatus.InGame;
            }
        }
    }
}
