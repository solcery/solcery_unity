using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Solcery.Utils
{
    public static class StreamingAsseter
    {
        public static void SaveBrickConfigs(BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + brickConfigs.name + ".json";

            string json = JsonConvert.SerializeObject(brickConfigs.ToData(), Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static async UniTask LoadBrickConfigs(string fileName, BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

            var txt = (await UnityWebRequest.Get(filePath).SendWebRequest()).downloadHandler.text;
            var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(txt);
            brickConfigs.FromData(brickConfigsData);

            // #if UNITY_EDITOR
            //             if (File.Exists(filePath))
            //             {
            //                 string fileContents = File.ReadAllText(filePath);
            //                 var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(fileContents);
            //                 brickConfigs.FromData(brickConfigsData);
            //             }
            // #endif
        }
    }
}

