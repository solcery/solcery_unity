using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickSubtypePopup : MonoBehaviour
    {
        [SerializeField] private GameObject optionPrefab = null;
        [SerializeField] private BrickConfigs brickConfigs = null;

        private Action<SubtypeNameConfig, UISelectBrickNode> _onOptionSelected;
        private List<UIBrickSubtypePopupOption> _options = new List<UIBrickSubtypePopupOption>();

        private UISelectBrickNode _button;

        public void Open(UISelectBrickNode button, Action<SubtypeNameConfig, UISelectBrickNode> onOptionSelected)
        {
            this.transform.SetAsLastSibling();

            _button = button;
            _onOptionSelected = onOptionSelected;

            var subTypeConfigs = brickConfigs.GetConfigSubtypeNamesByType(button.BrickType);

            if (subTypeConfigs != null && subTypeConfigs.Count > 0)
                foreach (var subTypeConfig in subTypeConfigs)
                    AddOption(subTypeConfig);


            // this.transform.position = button.transform.position;
            this.transform.position = new Vector2(button.transform.position.x + button.BrickWidth/2, button.transform.position.y);
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            ClearAllOptions();
        }

        private void OnOptionSelected(SubtypeNameConfig subtypeNameConfig)
        {
            _onOptionSelected?.Invoke(subtypeNameConfig, _button);
        }

        private void AddOption(SubtypeNameConfig subTypeName)
        {
            var newOption = Instantiate(optionPrefab, this.transform).GetComponent<UIBrickSubtypePopupOption>();
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
