using System;
using UnityEngine;

namespace Solcery.UI.Create.NodeEditor
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

            if (brickNodeHighlighted != null)
            {
                brickNodeHighlighted.Parent.Data.Slots[brickNodeHighlighted.IndexInParentSlots] = _buffer.Clone();
                _rebuild?.Invoke();
            }
        }
    }
}
