using Cysharp.Threading.Tasks;
using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace Solcery.NodeEditor.SM
{
    public class NodeEditorStateBehaviour : StateBehaviour
    {
        public override string StateID => stateId;
        [SerializeField] private string stateId;

        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();
        }

        protected override async UniTask OnExitState()
        {
            await base.OnExitState();
        }
    }
}

