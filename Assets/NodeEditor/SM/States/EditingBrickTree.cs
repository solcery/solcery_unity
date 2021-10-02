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
                // Reactives.SubscribeWithoutCurrent(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _stateCTS.Token);

                if (UINodeEditor.Instance != null)
                {
                    UINodeEditor.Instance.BrickTree.OnValidityChanged += OnBrickTreeValidityChange;
                    UINodeEditor.Instance.OnBrickInputChanged += OnBrickInputChanged;
                }
            }
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {
            _isCurrentBrickTreeValid = isValid;
            SaveIfValid();
        }

        private void OnBrickInputChanged()
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
