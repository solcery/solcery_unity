using UnityEngine;
using TMPro;

namespace Solcery.UI.Create.NodeEditor
{
    public class UIBrickField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fieldName = null;
        [SerializeField] private TMP_InputField fieldInput = null;

        public void Init(string fieldName, UIBrickFieldType fieldType, BrickData data)
        {
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
                UIBrickFieldType.String => TMP_InputField.ContentType.Name,
                _ => TMP_InputField.ContentType.Name
            };

            fieldInput.onValueChanged.AddListener((string input) =>
            {
                switch (fieldType)
                {
                    case UIBrickFieldType.Int:
                        if (System.Int32.TryParse(input, out var result))
                            data.IntField = result;
                        break;
                    case UIBrickFieldType.String:
                        data.StringField = input;
                        break;
                };
            });

            fieldInput.Select();
        }
    }
}