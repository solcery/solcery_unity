using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace Solcery
{
    public class GameStateBehaviour : StateBehaviour
    {
        public override string StateID => stateId;
        [SerializeField] private string stateId;
    }
}
