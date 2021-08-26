using Cysharp.Threading.Tasks;
using Solcery.Modules;

namespace Solcery
{
    public class InitStateBehaviour : PlayStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            var boardData = Board.Instance?.BoardData?.Value;

            if (boardData == null)
            {
                stateMachine.Trigger("OpenLobby");
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                stateMachine.Trigger("OpenLobby");
            }
            else
            {
                stateMachine.Trigger("OpenGame");
            }
        }
    }
}
