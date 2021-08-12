using Cysharp.Threading.Tasks;
using Solcery.FSM.Create;
using Solcery.UI.Create;
using Solcery.Utils;

namespace Solcery
{
    public class Create : Singleton<Create>
    {
        public async UniTask Init()
        {
            await UICreate.Instance.Init();
            CreateSM.Instance?.Enter();
        }

        public void DeInit()
        {
            UICreate.Instance?.DeInit();
        }
    }
}
