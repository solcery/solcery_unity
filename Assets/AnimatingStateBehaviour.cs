using Cysharp.Threading.Tasks;
using Solcery.UI;

namespace Solcery
{
    public class AnimatingStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            await UICardAnimator.Instance.LaunchAll();
            stateMachine?.Trigger("StopAnimating");
        }
    }
}