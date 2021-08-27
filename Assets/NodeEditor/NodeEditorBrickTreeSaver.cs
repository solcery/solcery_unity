using System.Threading;
using Solcery.UI.NodeEditor;
using Solcery.Utils;
using Solcery.Utils.Reactives;

namespace Solcery.NodeEditor
{
    public class NodeEditorBrickTreeSaver : Singleton<NodeEditorBrickTreeSaver>
    {
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            if (UINodeEditor.Instance != null)
            {
                Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);
                UINodeEditor.Instance.OnBrickInputChanged += OnBrickInputChanged;
            }
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {

        }

        private void OnBrickInputChanged()
        {
            UnityEngine.Debug.Log("Input changed");
        }
    }
}
