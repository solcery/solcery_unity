using Cysharp.Threading.Tasks;
using Solcery.UI;

namespace Solcery
{
    public class InitStateBehaviour : GameStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            UIGame.Instance?.Init();

            stateMachine.Trigger("StartGame");
        }
    }
}
