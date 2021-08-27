using Solcery.Utils;
using UnityEngine;

namespace Solcery.NodeEditor
{
    public class NodeEditorHotkeys : UpdateableSingleton<Hotkeys>
    {
        [SerializeField] private string testNodeEditorDataJson = null;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!string.IsNullOrEmpty(testNodeEditorDataJson))
                {
                    // var brickConfigsData = Newtonsoft.Json.JsonConvert.DeserializeObject<BrickConfigsData>(testNodeEditorDataJson);
                    // if (brickConfigsData == null)
                    //     Debug.Log("data is null");
                    NodeEditorReactToUnity.Instance?.SetNodeEditorData(testNodeEditorDataJson);
                }
            }
#endif
        }
    }
}