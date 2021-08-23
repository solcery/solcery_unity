using System;
using UnityEngine;

namespace Solcery.UI.NodeEditor
{
    public class UINodeEditorClipboard : MonoBehaviour
    {
        [SerializeField] private UINodeEditorClipboardInput input = null;

        private UINodeEditorNodeSelector _nodeSelector = null;
        private Action _rebuild;
        private BrickData _buffer = null;

        public void Init(UINodeEditorNodeSelector nodeSelector, Action rebuild)
        {
            _nodeSelector = nodeSelector;
            _rebuild = rebuild;

            input?.Init(OnCtrlXPressed, OnCtrlCPressed, OnCtrlVPressed);
        }

        public void DeInit()
        {
            input?.DeInit();
        }

        private void OnCtrlXPressed()
        {
            var brickNodeHighlighted = _nodeSelector?.BrickNodeHighlighted;
            _buffer = brickNodeHighlighted != null ? brickNodeHighlighted.Data : null;
            UINodeEditor.Instance.DeleteBrickNode(brickNodeHighlighted);

            _rebuild?.Invoke();
        }

        private void OnCtrlCPressed()
        {
            var brickNodeHighlighted = _nodeSelector?.BrickNodeHighlighted;
            _buffer = brickNodeHighlighted != null ? brickNodeHighlighted.Data : null;
        }

        private void OnCtrlVPressed()
        {
            var brickNodeHighlighted = _nodeSelector?.BrickNodeHighlighted;

            if (brickNodeHighlighted != null && brickNodeHighlighted.Data.Type == _buffer.Type)
            {
                if (brickNodeHighlighted.Parent != null)
                    // brickNodeHighlighted.Parent.Data.Slots[brickNodeHighlighted.IndexInParentSlots] = _buffer.Clone;
                    brickNodeHighlighted.Data.TurnInto(_buffer.Clone);
                else
                    UINodeEditor.Instance.BrickTree.SetGenesis(_buffer.Clone);

                _rebuild?.Invoke();
            }
        }
    }
}
