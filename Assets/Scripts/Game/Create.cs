using Grimmz.UI.Create;
using Grimmz.Utils;
using UnityEngine;

namespace Grimmz
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
