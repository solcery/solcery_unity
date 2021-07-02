using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UICreateRuleset : Singleton<UICreateRuleset>
    {
        [SerializeField] private Canvas canvas = null;

        public void Init()
        {

        }

        public void DeInit()
        {
            
        }

        public void Open()
        {
            canvas.enabled = true;
        }

        public void Close()
        {
            canvas.enabled = false;
        }
    }
}
