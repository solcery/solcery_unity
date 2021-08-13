using System.Threading;
using Cysharp.Threading.Tasks;
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

        public override async UniTask Enter()
        {
            await base.Enter();

            UILobby.Instance?.Init();

            _cts = new CancellationTokenSource();
            Reactives.Subscribe(PlayerGameStatusTracker.Instance?.PlayerStatus, OnPlayerStatusUpdate, _cts.Token);
        }

        public override async UniTask Exit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            UILobby.Instance?.DeInit();

            await base.Exit();
        }

        private void OnPlayerStatusUpdate(PlayerGameStatus playerStatus)
        {
            if (playerStatus == PlayerGameStatus.NotInGame)
                UILobby.Instance?.NotInGame();
            else if (playerStatus == PlayerGameStatus.WaitingForOpponent)
                UILobby.Instance?.WaitingForOpponent();
            else if (playerStatus == PlayerGameStatus.InGame)
                openGameTrigger?.Activate();
        }

    }
}
