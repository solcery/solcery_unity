using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.UI.Play.Game;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/Game", fileName = "Game")]
    public class GameState : PlayState
    {
        [SerializeField] private PlayTrigger openLobby = null;

        private CancellationTokenSource _cts;

        public override async UniTask Enter(Action<PlayTransition> performTransition)
        {
            await base.Enter(performTransition);

            BoardDataDiffTracker.Instance?.Init();
            GameResultTracker.Instance?.Init();
            UIGame.Instance?.Init();

            _cts = new CancellationTokenSource();
            Reactives.Subscribe(BoardDataDiffTracker.Instance?.BoardDataWithDiff, OnBoardUpdate, _cts.Token);
        }

        public override async UniTask Exit()
        {
            BoardDataDiffTracker.Instance?.DeInit();
            GameResultTracker.Instance?.DeInit();
            UIGame.Instance?.DeInit();

            await base.Exit();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                openLobby?.Activate();
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                openLobby?.Activate();
            }
            else
            {
                UIGame.Instance?.OnBoardUpdate(boardData);
            }
        }
    }
}
