using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Modules;
using Solcery.UI.Play.Lobby;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/Lobby", fileName = "Lobby")]
    public class LobbyState : PlayState
    {
        [SerializeField] private PlayTrigger openGameTrigger = null;

        private CancellationTokenSource _cts;

        public override async UniTask Enter(Action<PlayTransition> performTransition)
        {
            await base.Enter(performTransition);

            UILobby.Instance?.Init();

            _cts = new CancellationTokenSource();
            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
        }

        public override async UniTask Exit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            UILobby.Instance?.DeInit();

            await base.Exit();
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
                openGameTrigger?.Activate();
            }
        }
    }
}
