using Solcery.UI.NodeEditor;

namespace Solcery.NodeEditor.SM
{
    public class EditingBrickTree : NodeEditorStateBehaviour
    {
        private bool _isCurrentBrickTreeValid;

        protected override void OnEnterState()
        {
            base.OnEnterState();

            if (UINodeEditor.Instance != null)
            {
                _isCurrentBrickTreeValid = UINodeEditor.Instance.BrickTree.IsValid;
                UINodeEditor.Instance.BrickTree.OnValidityChanged += OnBrickTreeValidityChange;
                UINodeEditor.Instance.OnBrickInputChanged.AddListener(OnBrickInputChangedFired);
                // UnityEngine.Debug.Log(UINodeEditor.Instance.OnBrickInputChanged == null);
            }
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {
            _isCurrentBrickTreeValid = isValid;
            SaveIfValid();
        }

        private void OnBrickInputChangedFired()
        {
            SaveIfValid();
        }

        private void SaveIfValid()
        {
            if (_isCurrentBrickTreeValid)
                NodeEditorUnityToReact.Instance?.CallSaveBrickTree(UINodeEditor.Instance?.BrickTree);
        }
    }
}
