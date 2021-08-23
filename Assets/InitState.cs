using Cysharp.Threading.Tasks;
using Solcery.UI.NodeEditor;
using Solcery.Utils.Reactives;

namespace Solcery.NodeEditor.SM
{
    public class InitState : NodeEditorStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();
            Reactives.Subscribe(NodeEditor.Instance?.NodeEditorData, (nodeEditorData) => OnNodeEditorDataUpdate(nodeEditorData).Forget(), _stateCTS.Token);
        }

        private async UniTaskVoid OnNodeEditorDataUpdate(NodeEditorData nodeEditorData)
        {
            UnityEngine.Debug.Log("Update start");
            // setup the brickconfigs
            // open the brick tree
            await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
            await UINodeEditor.Instance.Init();

            UnityEngine.Debug.Log("Update end");
        }
    }
}
