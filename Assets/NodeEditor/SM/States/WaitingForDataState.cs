using Solcery.UI.NodeEditor;
using UnityEngine;

namespace Solcery.NodeEditor.SM
{
    public class WaitingForDataState : NodeEditorStateBehaviour
    {
        [SerializeField] private BrickConfigs brickConfigs = null;

        protected override void OnEnterState()
        {
            base.OnEnterState();
            UINodeEditor.Instance?.SetWaitingForData(true);

            if (NodeEditor.Instance != null)
                NodeEditor.Instance.OnNodeEditorDataChanged += OnNodeEditorDataUpdate;

            NodeEditorUnityToReact.Instance?.CallOnNodeEditorLoaded();
        }

        private void OnNodeEditorDataUpdate(NodeEditorData nodeEditorData)
        {
            if (nodeEditorData == null)
                return;

            if (nodeEditorData.BrickConfigsData == null)
                return;

            brickConfigs?.PopulateFromData(nodeEditorData.BrickConfigsData);
            UINodeEditor.Instance?.SetWaitingForData(false);
            UINodeEditor.Instance.Init(nodeEditorData.BrickTree, nodeEditorData.GenesisBrickType, true);
            stateMachine.Trigger("EditBrickTree");
        }
    }
}
