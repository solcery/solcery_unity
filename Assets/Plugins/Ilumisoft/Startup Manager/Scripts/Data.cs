namespace Ilumisoft.Plugins.StartupManager
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu]
    public class Data : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> prefabs = new List<GameObject>();

        public List<GameObject> Prefabs
        {
            get { return prefabs; }
        }
    }
}