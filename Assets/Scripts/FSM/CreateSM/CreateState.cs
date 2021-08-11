using Cysharp.Threading.Tasks;

namespace Solcery.FSM.Create
{
    public abstract class CreateState : State<CreateState, CreateTrigger, CreateTransition>
    {
        public override async UniTask Enter()
        {
            await UniTask.NextFrame();
        }

        public override async UniTask Exit()
        {
            await UniTask.NextFrame();
        }
    }
}
