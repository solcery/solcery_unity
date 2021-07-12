using System;
using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UIBrickSubtypePopup : UpdateableBehaviour
    {
        [SerializeField] private GameObject optionPrefab = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private ScrollRect scroll = null;

        private UISelectBrickNode _button;
        private Action<SubtypeNameConfig, UISelectBrickNode> _onOptionSelected;
        private bool _isOpen;
        private List<UIBrickSubtypePopupOption> _options = new List<UIBrickSubtypePopupOption>();

        public void Open(UISelectBrickNode button, BrickConfigs brickConfigs, Action<SubtypeNameConfig, UISelectBrickNode> onOptionSelected)
        {
            _isOpen = true;

            this.transform.SetAsLastSibling();

            _button = button;
            _onOptionSelected = onOptionSelected;

            var subTypeConfigs = brickConfigs.GetConfigSubtypeNamesByType(_button.BrickType);

            if (subTypeConfigs != null && subTypeConfigs.Count > 0)
                foreach (var subTypeConfig in subTypeConfigs)
                    AddOption(subTypeConfig);

            scroll.verticalNormalizedPosition = 1;

            // this.transform.position = new Vector2(button.transform.position.x + button.BrickWidth / 2, button.transform.position.y);
        }

        public void Close()
        {
            _isOpen = false;

            this.gameObject.SetActive(false);
            ClearAllOptions();
        }

        public override void PerformUpdate()
        {
            if (_isOpen)
                if (_button != null)
                    this.transform.position = new Vector2(_button.transform.position.x + _button.BrickWidth / 2, _button.transform.position.y);
        }

        private void OnOptionSelected(SubtypeNameConfig subtypeNameConfig)
        {
            _onOptionSelected?.Invoke(subtypeNameConfig, _button);
        }

        private void AddOption(SubtypeNameConfig subTypeName)
        {
            var newOption = Instantiate(optionPrefab, content).GetComponent<UIBrickSubtypePopupOption>();
            _options.Add(newOption);
            newOption?.Init(subTypeName, OnOptionSelected);
        }

        private void ClearAllOptions()
        {
            for (int i = _options.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(_options[i].gameObject);
            }

            _options = new List<UIBrickSubtypePopupOption>();
        }
    }
}
