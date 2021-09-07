using Cysharp.Threading.Tasks;
using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace Solcery
{
    public class PlayTransitionBehaviour : TransitionBehaviour
    {
        public override string TransitionId => transitionId;
        [SerializeField] protected string transitionId;

        public override async UniTask EnterTransition()
        {
            await base.EnterTransition();
        }

        public override async UniTask ExitTransition()
        {
            await base.ExitTransition();
        }
    }
}
