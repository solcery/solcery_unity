using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.NodeEditor
{
    public class NodeEditor : Singleton<NodeEditor>
    {
        public AsyncReactiveProperty<NodeEditorData> NodeEditorData => _nodeEditorData;
        private AsyncReactiveProperty<NodeEditorData> _nodeEditorData = new AsyncReactiveProperty<NodeEditorData>(null);

        public void UpdateData(NodeEditorData nodeEditorData)
        {
            if (nodeEditorData != null && nodeEditorData.BrickConfigsData != null)
                _nodeEditorData.Value = nodeEditorData;
        }
    }
}
