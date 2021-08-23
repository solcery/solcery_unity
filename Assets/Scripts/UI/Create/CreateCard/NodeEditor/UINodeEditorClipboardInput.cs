using System;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.NodeEditor
{
    public class UINodeEditorClipboardInput : UpdateableBehaviour
    {
        private bool ctrl = false;
        private bool x = false;
        private bool c = false;
        private bool v = false;
        private bool ctrlXPressed = false;
        private bool ctrlCPressed = false;
        private bool ctrlVPressed = false;

        private Action _onCtrlXPressed, _onCtrlCPressed, _onCtrlVPressed;

        public void Init(Action onCtrlXPressed, Action onCtrlCPressed, Action onCtrlVPressed)
        {
            _onCtrlXPressed = onCtrlXPressed;
            _onCtrlCPressed = onCtrlCPressed;
            _onCtrlVPressed = onCtrlVPressed;
        }

        public void DeInit()
        {
            _onCtrlXPressed = null;
            _onCtrlCPressed = null;
            _onCtrlVPressed = null;
        }

        public override void PerformUpdate()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ctrl = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                ctrl = false;
                ctrlXPressed = false;
                ctrlCPressed = false;
                ctrlVPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                c = true;
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                c = false;
                ctrlCPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                x = true;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                x = false;
                ctrlXPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                v = true;
            }

            if (Input.GetKeyUp(KeyCode.V))
            {
                v = false;
                ctrlVPressed = false;
            }

            if (ctrl && x && !ctrlXPressed)
            {
                _onCtrlXPressed?.Invoke();
                ctrlXPressed = true;
            }

            if (ctrl && c && !ctrlCPressed)
            {
                _onCtrlCPressed?.Invoke();
                ctrlCPressed = true;
            }

            if (ctrl && v && !ctrlVPressed)
            {
                _onCtrlVPressed?.Invoke();
                ctrlVPressed = true;
            }
        }
    }
}
