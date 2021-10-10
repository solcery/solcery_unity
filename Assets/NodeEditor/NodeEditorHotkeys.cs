using Solcery.Utils;
using UnityEngine;

namespace Solcery.NodeEditor
{
    public class NodeEditorHotkeys : UpdateableSingleton<NodeEditorHotkeys>
    {
        [SerializeField] [Multiline(20)] private string testNodeEditorDataJson = null;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!string.IsNullOrEmpty(testNodeEditorDataJson))
                    NodeEditorReactToUnity.Instance?.SetNodeEditorData(testNodeEditorDataJson);
            }
#endif
        }
    }
}