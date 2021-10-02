// using Cysharp.Threading.Tasks;
using Solcery.UI.NodeEditor;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.NodeEditor.SM
{
    public class WaitingForDataState : NodeEditorStateBehaviour
    {
        [SerializeField] private BrickConfigs brickConfigs = null;

        protected override void OnEnterState()
        {
            base.OnEnterState();
            NodeEditorUnityToReact.Instance?.CallOnNodeEditorLoaded();
            UINodeEditor.Instance?.SetWaitingForData(true);
            // Reactives.Subscribe(NodeEditor.Instance?.NodeEditorData, (nodeEditorData) => OnNodeEditorDataUpdate(nodeEditorData).Forget(), _stateCTS.Token);
            if (NodeEditor.Instance != null)
                NodeEditor.Instance.OnNodeEditorDataChanged += OnNodeEditorDataUpdate;
        }

        private void OnNodeEditorDataUpdate(NodeEditorData nodeEditorData)
        {
            if (nodeEditorData == null)
                return;

            if (nodeEditorData.BrickConfigsData == null)
                return;

            brickConfigs?.PopulateFromData(nodeEditorData.BrickConfigsData);
            UINodeEditor.Instance?.SetWaitingForData(false);
            UINodeEditor.Instance.Init(nodeEditorData.BrickTree, true);
            // await UniTask.WaitForEndOfFrame();
            stateMachine.Trigger("EditBrickTree");
        }
    }
}
