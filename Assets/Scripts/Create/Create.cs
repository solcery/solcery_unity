using Cysharp.Threading.Tasks;
using Solcery.FSM.Create;
using Solcery.UI.Create;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class Create : Singleton<Create>
    {
        public async UniTask Init()
        {
            Debug.Log("Create Init");
            await UICreate.Instance.Init();
            Debug.Log(CreateSM.Instance == null);
            CreateSM.Instance?.PerformInitialTransition();
        }

        public void DeInit()
        {
            UICreate.Instance?.DeInit();
        }
    }
}
