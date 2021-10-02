using System;
using System.Collections.Generic;
// using Cysharp.Threading.Tasks;
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

        public BrickConfigsData Create(Dictionary<BrickType, Dictionary<int, BrickConfig>> typeSubtype)
        {
            ConfigsByType = new Dictionary<BrickType, List<BrickConfigData>>();

            foreach (KeyValuePair<BrickType, Dictionary<int, BrickConfig>> entry in typeSubtype)
            {
                var subtypes = new List<BrickConfigData>();

                foreach (KeyValuePair<int, BrickConfig> subtype in entry.Value)
                {
                    subtypes.Add(subtype.Value.ToData());
                }

                ConfigsByType.Add(entry.Key, subtypes);
            }

            return this;
        }
    }

    [CreateAssetMenu(menuName = "Solcery/Bricks/BrickConfigs", fileName = "BrickConfigs")]
    public class BrickConfigs : SerializedScriptableObject
    {
        [SerializeField] [Newtonsoft.Json.JsonIgnore] private bool loadFromJson;
        [SerializeField] private Dictionary<BrickType, Dictionary<int, BrickConfig>> TypeSubtype = new Dictionary<BrickType, Dictionary<int, BrickConfig>>();

        public BrickConfigsData ToData()
        {
            var brickConfigsData = new BrickConfigsData();
            return brickConfigsData.Create(TypeSubtype);
        }

        public void PopulateFromData(BrickConfigsData data)
        {
            TypeSubtype = new Dictionary<BrickType, Dictionary<int, BrickConfig>>();

            foreach (KeyValuePair<BrickType, List<BrickConfigData>> entry in data.ConfigsByType)
            {
                var subtypeDict = new Dictionary<int, BrickConfig>();

                foreach (var configData in entry.Value)
                {
                    var config = ScriptableObject.CreateInstance<BrickConfig>();
                    config.name = configData.Name;
                    config.FromData(configData);
                    subtypeDict.Add(config.Subtype, config);
                }

                TypeSubtype.Add(entry.Key, subtypeDict);
            }
        }

        public Dictionary<int, BrickConfig> GetConfigsByType(BrickType brickType)
        {
            if (TypeSubtype.TryGetValue(brickType, out var subtypeDict))
            {
                return subtypeDict;
            }

            return null;
        }

        public List<SubtypeNameConfig> GetConfigSubtypeNamesByType(BrickType brickType)
        {
            var configsOfType = GetConfigsByType(brickType);

            if (configsOfType != null && configsOfType.Count > 0)
            {
                var names = new List<SubtypeNameConfig>();

                foreach (var entry in configsOfType)
                {
                    names.Add(new SubtypeNameConfig(entry.Value.Name, entry.Value));
                }

                return names;
            }

            return null;
        }

        public BrickConfig GetConfigByTypeAndSubtype(BrickType type, int subType)
        {
            var configsOfType = GetConfigsByType(type);

            if (configsOfType.TryGetValue(subType, out var config))
                return config;

            return null;
        }

        public static BrickType GetType(int typeIndex)
        {
            return (BrickType)typeIndex;
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

        public void Init()
        {
            if (loadFromJson)
                StreamingAsseter.Instance.Load(fileName, this, PopulateFromData);
        }

        [Button(ButtonSizes.Gigantic)]
        private void SaveToStreamingAssets()
        {
            StreamingAsseter.SaveBrickConfigs(fileName, this);
        }

        [Button(ButtonSizes.Gigantic)]
        private void LoadFromStreamingAssets()
        {
            StreamingAsseter.Instance?.Load(fileName, this, PopulateFromData);
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
