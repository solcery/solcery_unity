using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.UI.Create
{
    public class UICreate : Singleton<UICreate>
    {
        public async UniTask Init()
        {
            await UICreateCard.Instance.Init();
        }

        public void DeInit()
        {
            UICreateCard.Instance.DeInit();
        }
    }
}
