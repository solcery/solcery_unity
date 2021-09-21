using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.NodeEditor
{
    public class NodeEditor : Singleton<NodeEditor>
    {
        public AsyncReactiveProperty<NodeEditorData> NodeEditorData => _nodeEditorData;
        private AsyncReactiveProperty<NodeEditorData> _nodeEditorData = new AsyncReactiveProperty<NodeEditorData>(null);

        void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = false;
#endif
        }

        public void UpdateData(NodeEditorData nodeEditorData)
        {
            _nodeEditorData.Value = nodeEditorData;
        }
    }
}
