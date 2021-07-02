using Cysharp.Threading.Tasks;
using Solcery.UI.Create;
using Solcery.Utils;

namespace Solcery
{
    public class Create : Singleton<Create>
    {
        public async UniTask Init()
        {
            await UICreateCard.Instance.Init();
        }

        public void DeInit()
        {
            UICreateCard.Instance?.DeInit();
        }
    }
}
