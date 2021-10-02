using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Solcery.Utils
{
    public static class StreamingAsseter
    {
        public static void SaveBrickConfigs(string fileName, BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

            // var nodeEditorData = new NodeEditor.NodeEditorData();
            // nodeEditorData.BrickTree = null;
            // nodeEditorData.BrickConfigsData = brickConfigs.ToData();

            // string json = JsonConvert.SerializeObject(nodeEditorData, Formatting.Indented);
            string json = JsonConvert.SerializeObject(brickConfigs.ToData(), Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static async UniTask LoadBrickConfigs(string fileName, BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

            var txt = (await UnityWebRequest.Get(filePath).SendWebRequest()).downloadHandler.text;
            var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(txt);
            brickConfigs.PopulateFromData(brickConfigsData);
        }
    }
}
