using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/States/Init", fileName = "Init")]
    public class InitState : PlayState
    {
        [SerializeField] private PlayTrigger openLobby = null;
        [SerializeField] private PlayTrigger openGame = null;

        public override async UniTask Enter()
        {
            await base.Enter();

            var boardData = BoardDataDiffTracker.Instance?.BoardDataWithDiff?.Value;

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
