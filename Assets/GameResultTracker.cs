using System.Threading;
using Solcery.Modules;
using Solcery.Utils;
using Solcery.Utils.Reactives;

namespace Solcery
{
    public class GameResultTracker : Singleton<GameResultTracker>
    {
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null) return;

            var me = boardData.Me;
            if (me == null) return;

            var enemy = boardData.Enemy;
            if (enemy == null) return;

            var myId = boardData.Me.PlayerId;
            var enemyId = boardData.Enemy.PlayerId;

            if (enemy.HP <= 0)
            {
                // Victory
                LogActionCreator.Instance?.DeclareVictory(myId);
                return;
            }

            if (me.HP <= 0)
            {
                // Defeat
                LogActionCreator.Instance?.DeclareDefeat(myId);
                return;
            }

            /// BOTH HAVE POSITIVE HP, CHECK WHAT'S UP

            if (me.Status == PlayerStatus.Undefined)
            {
                // My status is undefined. ???. Leave game
                return;
            }

            if (me.Status == PlayerStatus.Offline)
            {
                // My status is offline. I was probably AFK. ???. Leave game if enemy declared victory or defeat, otherwise set status to Online and play.
                return;
            }

            /// MY STATUS IS ONLINE FROM HERE

            if (me.Outcome == PlayerOutcome.Victory)
            {
                // I have declared victory already. Set status to offline. Leave game
                LogActionCreator.Instance?.SetStatus(myId, PlayerStatus.Offline);
                return;
            }

            if (me.Outcome == PlayerOutcome.Defeat)
            {
                // I have declared defeat already. Set status to offline. Leave game
                LogActionCreator.Instance?.SetStatus(myId, PlayerStatus.Offline);
                return;
            }

            /// MY OUTCOME IS UNDEFINED FROM HERE

            if (enemy.Status == PlayerStatus.Undefined)
            {
                // wtf. Declare victory. Leave game
                LogActionCreator.Instance?.DeclareVictory(myId);
                return;
            }

            if (enemy.Status == PlayerStatus.Offline)
            {
                // Enemy is offline. Declare victory. Leave game
                LogActionCreator.Instance?.DeclareVictory(myId);
                return;
            }

            /// ENEMY STATUS IS ONLINE FROM HERE

            if (enemy.Outcome == PlayerOutcome.Defeat)
            {
                // Enemy is online and declared defeat. Declare victory. Leave game.
                LogActionCreator.Instance?.DeclareVictory(myId);
                return;
            }

            if (enemy.Outcome == PlayerOutcome.Victory)
            {
                // Enemy is online and declared victory. That's BULLSHIT. Declare victory. Leave game.
                LogActionCreator.Instance?.DeclareVictory(myId);
                return;
            }

            /// BOTH ARE ONLINE AND BOTH HAVE UNDEFINED OUTCOMES. LET THE GAME CONTINUE.
            UnityEngine.Debug.Log("GAME GOES ON!");
        }
    }
}
