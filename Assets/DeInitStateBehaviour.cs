using Cysharp.Threading.Tasks;
using Solcery.UI;

namespace Solcery
{
    public class DeInitStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            GameStateDiffTracker.Instance?.DeInit();
            UIGame.Instance?.DeInit();

            stateMachine.Trigger("DeInit");
        }
    }
}
