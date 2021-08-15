using System;
using Cysharp.Threading.Tasks;
using Solcery.Modules;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/_Init", fileName = "_Init")]
    public class _InitState : PlayState
    {
        [SerializeField] private PlayTrigger openLobby = null;
        [SerializeField] private PlayTrigger openGame = null;

        public override async UniTask Enter(Action<PlayTransition> performTransition)
        {
            await base.Enter(performTransition);

            var boardData = Board.Instance?.BoardData?.Value;

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
                openGame?.Activate();
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}
