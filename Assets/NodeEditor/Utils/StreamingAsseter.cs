using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace Solcery.Utils
{
    public class StreamingAsseter : Singleton<StreamingAsseter>
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

        // public static void LoadBrickConfigs(string fileName, BrickConfigs brickConfigs)
        // {
        //     var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

        //     var txt = (await UnityWebRequest.Get(filePath).SendWebRequest()).downloadHandler.text;
        //     var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(txt);
        //     brickConfigs.PopulateFromData(brickConfigsData);
        // }

        public void Load(string fileName, BrickConfigs brickConfigs, Action<BrickConfigsData> onLoaded)
        {
            StartCoroutine(LoadBrickConfigs(fileName, brickConfigs, onLoaded));
        }

        private IEnumerator LoadBrickConfigs(string fileName, BrickConfigs brickConfigs, Action<BrickConfigsData> onLoaded)
        {
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

            UnityWebRequest www = new UnityWebRequest(filePath);
            yield return www;
            var txt = www.downloadHandler.text;
            var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(txt);
            onLoaded?.Invoke(brickConfigsData);
            // brickConfigs.PopulateFromData(brickConfigsData);
        }
    }
}
