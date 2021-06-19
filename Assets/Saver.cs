using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Solcery.Utils
{
    public static class Saver
    {
        public static void SaveBrickConfigs(BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + brickConfigs.name + ".json";

            string json = JsonConvert.SerializeObject(brickConfigs.ToData(), Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static void LoadBrickConfigs(string fileName, BrickConfigs brickConfigs)
        {
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";

            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                var brickConfigsData = JsonConvert.DeserializeObject<BrickConfigsData>(fileContents);
                brickConfigs.FromData(brickConfigsData);
            }
        }

        // public void SaveConfig()
        // {
        //     var filePath = Application.streamingAssetsPath + "/TestBrickConfig.json";

        //     string json = JsonConvert.SerializeObject(brickConfig.ToData());
        //     File.WriteAllText(filePath, json);
        // }

        // public void LoadConfig()
        // {
        //     var filePath = Application.streamingAssetsPath + "/TestBrickConfig.json";

        //     if (File.Exists(filePath))
        //     {
        //         string fileContents = File.ReadAllText(filePath);
        //         var brickConfigData = JsonConvert.DeserializeObject<BrickConfigData>(fileContents);
        //         testConfig.FromData(brickConfigData);
        //     }
        // }
    }
}

