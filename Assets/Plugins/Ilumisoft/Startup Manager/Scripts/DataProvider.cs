namespace Ilumisoft.Plugins.StartupManager
{
    using UnityEngine;
    
    public static class DataProvider
    {
        /// <summary>
        /// The path of the scriptable object containing the data in the resources folder
        /// </summary>
        private const string ResourcesPath = "Ilumisoft/Startup Manager/Data";
        
        public static Data GetData()
        {
            //Try to find active instances
            var instances= Resources.FindObjectsOfTypeAll<Data>();

            //If none found, load it, otherwise use first
            var data = instances.Length == 0 ? Resources.Load<Data>(ResourcesPath) : instances[0];

            return data;
        }
    }
}