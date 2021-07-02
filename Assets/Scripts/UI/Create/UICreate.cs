using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UICreate : Singleton<UICreate>
    {
        [SerializeField] private UICreateTabs tabs = null;

        public async UniTask Init()
        {
            tabs?.Init();

            await UICreateCard.Instance.Init();
            UICreateRuleset.Instance?.Init();
        }

        public void DeInit()
        {
            tabs?.DeInit();

            UICreateCard.Instance?.DeInit();
            UICreateRuleset.Instance?.DeInit();
        }
    }
}
