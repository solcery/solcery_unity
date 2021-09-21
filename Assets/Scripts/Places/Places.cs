using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Places/Places", fileName = "Places")]
    public class Places : SerializedScriptableObject
    {
        [SerializeField]
        public Dictionary<CardLayoutOption, GameObject> PlacePrefabs;
    }
}
