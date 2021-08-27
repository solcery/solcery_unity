using Cysharp.Threading.Tasks;
using Solcery.UI.NodeEditor;
using Solcery.Utils.Reactives;

namespace Solcery.NodeEditor.SM
{
    public class EditingBrickTree : NodeEditorStateBehaviour
    {
        protected override async UniTask OnEnterState()
        {
            await base.OnEnterState();

            if (UINodeEditor.Instance != null)
            {
                Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _stateCTS.Token);
                UINodeEditor.Instance.OnBrickInputChanged += OnBrickInputChanged;
            }
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {
            UnityEngine.Debug.Log("Validity changed");
        }

        private void OnBrickInputChanged()
        {
            UnityEngine.Debug.Log("Input changed");
        }
    }
}
