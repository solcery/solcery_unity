/*
    RuntimeManager:
    Instantiates all prefabs added via the Startup Manager right before the first scene is loaded.
*/

namespace Ilumisoft.Plugins.StartupManager
{
    using System.Collections.Generic;
    using UnityEngine;

    internal static class RuntimeManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeOnLoad()
        {
            var data = DataProvider.GetData();

            InstantiatePrefabs(data.Prefabs);
        }

        private static void InstantiatePrefabs(List<GameObject> prefabs)
        {
            foreach (var prefab in prefabs)
            {
                InstantiatePrefab(prefab);
            }
        }

        private static void InstantiatePrefab(GameObject prefab)
        {
            if (prefab != null)
            {
                var prefabInstance = Object.Instantiate(prefab);

                prefabInstance.name = prefab.name;

                Object.DontDestroyOnLoad(prefabInstance);
            }
        }
    }
}