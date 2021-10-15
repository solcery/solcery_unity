using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

namespace Solcery.UI.NodeEditor
{
    public class UIBrickField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fieldName = null;
        [SerializeField] private TMP_InputField fieldInput = null;

        private UnityEvent _onBrickInputChanged;

        public void Init(string fieldName, UIBrickFieldType fieldType, BrickData data, UnityEvent brickInputChanged)
        {
            _onBrickInputChanged = brickInputChanged;

            this.fieldName.text = fieldName;

            fieldInput.text = fieldType switch
            {
                UIBrickFieldType.Int => data.IntField.ToString(),
                UIBrickFieldType.String => data.StringField,
                _ => data.IntField.ToString()
            };

            fieldInput.contentType = fieldType switch
            {
                UIBrickFieldType.Int => TMP_InputField.ContentType.IntegerNumber,
                UIBrickFieldType.String => TMP_InputField.ContentType.Standard,
                _ => TMP_InputField.ContentType.Name
            };

            fieldInput.onValueChanged.AddListener((string input) =>
            {
                switch (fieldType)
                {
                    case UIBrickFieldType.Int:
                        if (System.Int32.TryParse(input, out var result))
                        {
                            data.IntField = result;
                            _onBrickInputChanged?.Invoke();
                        }
                        break;
                    case UIBrickFieldType.String:
                        data.StringField = input;
                        _onBrickInputChanged?.Invoke();
                        break;
                };
            });

            fieldInput.Select();
        }
    }
}
