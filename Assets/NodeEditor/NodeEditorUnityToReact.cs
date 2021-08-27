using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Solcery.Utils;

namespace Solcery.NodeEditor
{
    public class NodeEditorUnityToReact : Singleton<NodeEditorUnityToReact>
    {
        [DllImport("__Internal")] private static extern void SaveBrickTree(string brickTree);

        public void CallSaveBrickTree(BrickTree brickTree)
        {
            var brickTreeJson = JsonConvert.SerializeObject(brickTree);

#if (UNITY_WEBGL && !UNITY_EDITOR)
            SaveBrickTree(brickTreeJson);
#endif
        }
    }
}