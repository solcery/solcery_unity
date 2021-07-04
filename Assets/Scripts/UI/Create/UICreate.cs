using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UICreate : Singleton<UICreate>
    {
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private UICreateTabs tabs = null;

        public async UniTask Init()
        {
            tabs?.Init();

            UICollection.Instance?.Init(canvas);
            await UICreateCard.Instance.Init();
            UICreateRuleset.Instance?.Init();
        }

        public void DeInit()
        {
            tabs?.DeInit();

            UICollection.Instance?.DeInit();
            UICreateCard.Instance?.DeInit();
            UICreateRuleset.Instance?.DeInit();
        }
    }
}
