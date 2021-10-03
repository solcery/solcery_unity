using System;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.NodeEditor
{
    public class NodeEditor : Singleton<NodeEditor>
    {
        public Action<NodeEditorData> OnNodeEditorDataChanged;
        public NodeEditorData NodeEditorData => _nodeEditorData;
        private NodeEditorData _nodeEditorData;

        void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = true;
#endif
        }

        public void UpdateData(NodeEditorData nodeEditorData)
        {
            _nodeEditorData = nodeEditorData;
            OnNodeEditorDataChanged?.Invoke(_nodeEditorData);
        }
    }
}
