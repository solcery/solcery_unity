using Cysharp.Threading.Tasks;
using Solcery.UI.Create.NodeEditor;

namespace Solcery.SM.NodeEditor
{
    public class InitState : NodeEditorStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            await UINodeEditor.Instance.Init();
        }
    }
}
