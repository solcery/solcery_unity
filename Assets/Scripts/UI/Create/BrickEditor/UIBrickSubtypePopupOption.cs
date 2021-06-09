using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickSubtypePopupOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionName = null;
        [SerializeField] private Button button = null;

        public void Init(SubtypeNameConfig subtypeNameConfig, Action<SubtypeNameConfig> onSelect)
        {
            SetName(subtypeNameConfig.Name);
            button.onClick.AddListener(() => onSelect?.Invoke(subtypeNameConfig));
        }

        private void SetName(string name)
        {
            if (optionName != null)
                optionName.text = name;
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
