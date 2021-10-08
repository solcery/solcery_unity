using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace Solcery
{
    public class GameTransitionBehaviour : TransitionBehaviour
    {
        public override string TransitionId => transitionId;
        [SerializeField] protected string transitionId;
    }
}
