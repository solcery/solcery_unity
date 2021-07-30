using System.Threading;
using Solcery.Modules;
using Solcery.UI.Play;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine.Assertions;

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
            UnityEngine.Debug.Log("GameResultTracker.OnBoardUpdate");

            if (boardData == null) return;
            if (boardData.IsVirgin) return;

            var me = boardData.Me;
            if (me == null) return;

            var enemy = boardData.Enemy;
            if (enemy == null) return;

            var myIndex = boardData.MyIndex;
            var enemyIndex = boardData.EnemyIndex;

            //TODO: get real ids instead of index + 1
            var myId = myIndex + 1; // var myId = boardData.Me.PlayerId;
            var enemyId = enemyIndex + 1; // var enemyId = boardData.Enemy.PlayerId;

            UnityEngine.Debug.Log($"My Status: {me.Status}");
            UnityEngine.Debug.Log($"My Outcome: {me.Outcome}");
            UnityEngine.Debug.Log($"Enemy Status: {enemy.Status}");
            UnityEngine.Debug.Log($"Enemy Outcome: {enemy.Outcome}");

#if (UNITY_WEBGL && !UNITY_EDITOR)
            Assert.AreNotEqual(me.Status, PlayerStatus.Undefined);
            Assert.AreNotEqual(enemy.Status, PlayerStatus.Undefined);
#endif

            if (me.Status == PlayerStatus.Offline || enemy.Status == PlayerStatus.Offline)
            {
                GameOverPopup("Game Over", "This game has ended");
                return;
            }

            if (me.Outcome != PlayerOutcome.Undefined)
            {
                GameOverPopup("Game Over", "This game has ended");
                return;
            }

            if (enemy.HP <= 0)
            {
                GameOverPopup("Victory", "You have won!", myId, true, PlayerOutcome.Victory);
                return;
            }

            if (me.HP <= 0)
            {
                GameOverPopup("Defeat", "You have lost...", myId, true, PlayerOutcome.Defeat);
                return;
            }

            /// BOTH ARE ONLINE, BOTH HAVE UNDEFINED OUTCOMES AND BOTH HAVE HP > 0. LET THE GAME CONTINUE.
            UnityEngine.Debug.Log("GAME GOES ON!");
        }

        private void GameOverPopup(string title = null, string description = null, int playerId = 0, bool hasOutcome = false, PlayerOutcome outcome = PlayerOutcome.Undefined)
        {
            UIGameOverPopup.Instance?.OpenWithDelay(1.5f, new GameOverData(title, description, () =>
            {
                Board.Instance?.UpdateBoard(null);
                LogActionCreator.Instance.LeaveGame(playerId, hasOutcome, outcome);
            }));
        }
    }
}
