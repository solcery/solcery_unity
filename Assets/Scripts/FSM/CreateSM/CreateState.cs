using System;
using Cysharp.Threading.Tasks;

namespace Solcery.FSM.Create
{
    public abstract class CreateState : State<CreateState, CreateTrigger, CreateTransition>
    {
        public override async UniTask Enter(Action<CreateTransition> performTransition)
        {
            await base.Enter(performTransition);
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}
