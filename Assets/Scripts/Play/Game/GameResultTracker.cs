using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Modules;
using Solcery.UI.Play;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solcery
{
    public class GameResultTracker : Singleton<GameResultTracker>
    {
        [SerializeField] private PlayerAFKTimer enemyAFKTimer = null;
        [SerializeField] private PlayerAFKTimer playerAFKTimer = null;

        private AsyncReactiveProperty<bool> _isEnemyActive = new AsyncReactiveProperty<bool>(false);
        private AsyncReactiveProperty<bool> _isPlayerActive = new AsyncReactiveProperty<bool>(false);

        private CancellationTokenSource _cts;
        private int _myId;
        private int _enemyId;
        public void Init()
        {
            _cts = new CancellationTokenSource();

            enemyAFKTimer?.Init(_isEnemyActive, () => GameOverPopup(0f, "Victory", "Your opponent was inactive for far too long", _myId, true, PlayerOutcome.Victory));
            playerAFKTimer?.Init(_isPlayerActive, null);

            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
        }
        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            enemyAFKTimer?.DeInit();
            playerAFKTimer?.DeInit();
        }
        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null) return;

            var me = boardData.Me;
            if (me == null) return;

            var enemy = boardData.Enemy;
            if (enemy == null) return;

            if (_isEnemyActive.Value != boardData.Enemy.IsActive)
                _isEnemyActive.Value = boardData.Enemy.IsActive;

            if (_isPlayerActive.Value != boardData.Me.IsActive)
                _isPlayerActive.Value = boardData.Me.IsActive;

            //TODO: pizdec
            if (boardData.IsVirgin) return;

            var myIndex = boardData.MyIndex;
            var enemyIndex = boardData.EnemyIndex;

            //TODO: get real ids instead of index + 1
            _myId = myIndex + 1; // var myId = boardData.Me.PlayerId;
            _enemyId = enemyIndex + 1; // var enemyId = boardData.Enemy.PlayerId;

#if (UNITY_WEBGL && !UNITY_EDITOR)
            Assert.AreNotEqual(me.Status, PlayerStatus.Undefined);
            Assert.AreNotEqual(enemy.Status, PlayerStatus.Undefined);
#endif

            if (me.Status == PlayerStatus.Offline || enemy.Status == PlayerStatus.Offline)
            {
                GameOverPopup(2f, "Game Over", "This game has ended");
                return;
            }

            if (me.Outcome != PlayerOutcome.Undefined)
            {
                GameOverPopup(2f, "Game Over", "This game has ended");
                return;
            }

            if (enemy.HP <= 0)
            {
                GameOverPopup(2f, "Victory", "You have won!", _myId, true, PlayerOutcome.Victory);
                return;
            }

            if (me.HP <= 0)
            {
                GameOverPopup(2f, "Defeat", "You have lost...", _myId, true, PlayerOutcome.Defeat);
                return;
            }

            /// BOTH ARE ONLINE, BOTH HAVE UNDEFINED OUTCOMES AND BOTH HAVE HP > 0. LET THE GAME CONTINUE.
        }

        private void GameOverPopup(float delay, string title = null, string description = null, int playerId = 0, bool hasOutcome = false, PlayerOutcome outcome = PlayerOutcome.Undefined)
        {
            UIGameOverPopup.Instance?.OpenWithDelay(delay, new GameOverData(title, description, () =>
            {
                Board.Instance?.UpdateBoard(null);
                LogActionCreator.Instance.LeaveGame(playerId, hasOutcome, outcome);
                Log.Instance?.UpdateLog(null);
            }));
        }
    }
}
