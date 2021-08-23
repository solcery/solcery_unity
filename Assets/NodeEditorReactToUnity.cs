using Newtonsoft.Json;
using Solcery.Utils;

namespace Solcery.NodeEditor
{
    public class NodeEditorReactToUnity : Singleton<NodeEditorReactToUnity>
    {
        public void SetNodeEditorData(string nodeEditorDataJson)
        {
            var nodeEditorData = JsonConvert.DeserializeObject<NodeEditorData>(nodeEditorDataJson);
            NodeEditor.Instance?.UpdateData(nodeEditorData);
        }
    }
}
