using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    [Serializable]
    public class BrickConfigsData
    {
        [SerializeField]
        public Dictionary<BrickType, List<BrickConfigData>> ConfigsByType;

        public BrickConfigsData Create(Dictionary<BrickType, List<BrickConfig>> configsByType)
        {
            ConfigsByType = new Dictionary<BrickType, List<BrickConfigData>>();

            foreach (KeyValuePair<BrickType, List<BrickConfig>> entry in configsByType)
            {
                var list = new List<BrickConfigData>();

                foreach (var config in entry.Value)
                {
                    list.Add(config.ToData());
                }

                ConfigsByType.Add(entry.Key, list);
            }

            return this;
        }
    }

    [CreateAssetMenu(menuName = "Solcery/Bricks/BrickConfigs", fileName = "BrickConfigs")]
    public class BrickConfigs : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<BrickType, List<BrickConfig>> ConfigsByType = new Dictionary<BrickType, List<BrickConfig>>();

        public BrickConfigsData ToData()
        {
            var brickConfigsData = new BrickConfigsData();
            return brickConfigsData.Create(ConfigsByType);
        }

        public void FromData(BrickConfigsData data)
        {
            ConfigsByType = new Dictionary<BrickType, List<BrickConfig>>();

            foreach (KeyValuePair<BrickType, List<BrickConfigData>> entry in data.ConfigsByType)
            {
                var list = new List<BrickConfig>();

                foreach (var configData in entry.Value)
                {
                    var config = ScriptableObject.CreateInstance<BrickConfig>();
                    config.name = configData.Name;
                    config.FromData(configData);
                    list.Add(config);
                }

                ConfigsByType.Add(entry.Key, list);
            }
        }

        public List<BrickConfig> GetConfigsByType(BrickType brickType)
        {
            if (ConfigsByType.TryGetValue(brickType, out var datas))
            {
                return datas;
            }

            return null;
        }

        public List<SubtypeNameConfig> GetConfigSubtypeNamesByType(BrickType brickType)
        {
            var configsOfType = GetConfigsByType(brickType);

            if (configsOfType != null && configsOfType.Count > 0)
            {
                var names = new List<SubtypeNameConfig>();

                foreach (var config in configsOfType)
                {
                    names.Add(new SubtypeNameConfig(config.Name, config));
                }

                return names;
            }

            return null;
        }

        public BrickConfig GetConfigByTypeAndSubtype(BrickType type, int subType)
        {
            var configsOfType = GetConfigsByType(type);
            return configsOfType[subType];
        }

        public static int GetSubtypeIndex(BrickType brickType, Enum subType)
        {
            return brickType switch
            {
                BrickType.Action => (int)((BrickSubtypeAction)subType),
                BrickType.Condition => (int)((BrickSubtypeCondition)subType),
                BrickType.Value => (int)((BrickSubtypeValue)subType),
                _ => (int)((BrickSubtypeValue)subType)
            };
        }

        public static string GetSubtypeName(BrickType brickType, Enum subtype)
        {
            return brickType switch
            {
                BrickType.Action => Enum.GetName(typeof(BrickSubtypeAction), subtype),
                BrickType.Condition => Enum.GetName(typeof(BrickSubtypeCondition), subtype),
                BrickType.Value => Enum.GetName(typeof(BrickSubtypeValue), subtype),
                _ => Enum.GetName(typeof(BrickSubtypeValue), subtype)
            };
        }

        public async UniTask Init()
        {
            await StreamingAsseter.LoadBrickConfigs(fileName, this);
        }

        [Button(ButtonSizes.Gigantic)]
        private void SaveToStreamingAssets()
        {
            StreamingAsseter.SaveBrickConfigs(this);
        }

        [Button(ButtonSizes.Gigantic)]
        private void LoadFromStreamingAssets()
        {
            StreamingAsseter.LoadBrickConfigs(fileName, this).Forget();
        }

        [SerializeField] private string fileName;
    }

    public struct SubtypeNameConfig
    {
        public string Name;
        public BrickConfig Config;

        public SubtypeNameConfig(string name, BrickConfig config)
        {
            Name = name;
            Config = config;
        }
    }
}
