using Cysharp.Threading.Tasks;
using Solcery.UI;

namespace Solcery
{
    public class InitStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            GameStateDiffTracker.Instance?.Init();
            UIGame.Instance?.Init();

            stateMachine.Trigger("StartGame");
        }
    }
}
