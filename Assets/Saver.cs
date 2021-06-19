using System.IO;
using UnityEngine;

namespace Solcery.Utils
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private BrickConfigs brickConfigs = null;
        [SerializeField] private BrickConfig testConfig = null;

        public void Save()
        {
            var filePath = Application.streamingAssetsPath + "/TestBrickConfigs.json";
            Debug.Log(brickConfigs.ToData().ConfigsByType.Count);
            string json = JsonUtility.ToJson(brickConfigs.ToData());
            File.WriteAllText(filePath, json);
        }

        public void Load()
        {

        }

        void Start()
        {
            Save();
        }
    }
}

