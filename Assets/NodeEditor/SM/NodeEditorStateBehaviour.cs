using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace Solcery.NodeEditor.SM
{
    public class NodeEditorStateBehaviour : StateBehaviour
    {
        public override string StateID => stateId;
        [SerializeField] private string stateId;

        protected override void OnEnterState()
        {
            base.OnEnterState();
        }

        protected override void OnExitState()
        {
            base.OnExitState();
        }
    }
}

