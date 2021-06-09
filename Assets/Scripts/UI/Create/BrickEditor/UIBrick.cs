using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrick : MonoBehaviour
    {
        public BrickData Data { get; private set; }
        public UIBrickSlots Slots => slots;
        public UIBrick Parent { get; private set; }
        public int IndexInParentSlots { get; private set; }
        public Transform Vert { get; private set; }
        public GameObject Hor { get; private set; }
        public BrickConfig Config { get; private set; }

        [SerializeField] private Button closeButton = null;
        [SerializeField] private TextMeshProUGUI typeName = null;
        [SerializeField] private TextMeshProUGUI subtypeName = null;
        [SerializeField] private TextMeshProUGUI description = null;

        [SerializeField] private UIBrickField field = null;

        [SerializeField] private UIBrickObjectSwitcher objectSwitcher = null;
        [SerializeField] private UIBrickSlots slots = null;

        public void Init(BrickConfig config, BrickData data, UIBrick parent, int indexInParentSlots, Transform vert, GameObject hor)
        {
            Config = config;
            Data = data;
            Parent = parent;
            IndexInParentSlots = indexInParentSlots;
            Vert = vert;
            Hor = hor;

            typeName.text = Enum.GetName(typeof(BrickType), config.Type);
            subtypeName.text = BrickConfigs.GetSubtypeName(config.Type, config.Subtype);
            description.text = config.Description;

            field.gameObject.SetActive(config.HasField);
            if (config.HasField) field.Init(config.FieldName, config.FieldType, data);

            objectSwitcher.gameObject.SetActive(config.HasObjectSelection);
            objectSwitcher.Init(data);
            slots.Init(config.Slots);

            closeButton.onClick.AddListener(() =>
            {
                UICreate.Instance.BrickEditor.DeleteBrick(this);
            });
        }
    }
}
