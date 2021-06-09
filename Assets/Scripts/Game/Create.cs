using Solcery.UI.Create;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class Create : Singleton<Create>
    {
        public void Init()
        {
            // Debug.Log("Create Init");
            UICreate.Instance.Init();
        }

        public void DeInit()
        {
            // Debug.Log("Create DeInit");
            UICreate.Instance.DeInit();
        }
    }
}
