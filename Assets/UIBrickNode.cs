using System;
using Solcery;
using Solcery.UI.Create.BrickEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBrickNode : UINode
{
    public BrickConfig Config { get; private set; }
    public BrickData Data { get; private set; }
    public UIBrickNode Parent { get; private set; }
    public int IndexInParentSlots { get; private set; }
    public UIBrickSlots Slots => slots;

    [SerializeField] private Button deleteButton = null;
    [SerializeField] private TextMeshProUGUI typeName = null;
    [SerializeField] private TextMeshProUGUI subtypeName = null;
    [SerializeField] private TextMeshProUGUI description = null;
    [SerializeField] private UIBrickField field = null;
    [SerializeField] private UIBrickObjectSwitcher objectSwitcher = null;
    [SerializeField] private UIBrickSlots slots = null;

    public void Init(BrickConfig config, BrickData data, UIBrickNode parent, int indexInParentSlots)
    {
        Config = config;
        Data = data;
        Parent = parent;
        IndexInParentSlots = indexInParentSlots;

        NodeSlots = new UINode[config.Slots.Count];
        Arrows = new TestArrow[config.Slots.Count];

        typeName.text = Enum.GetName(typeof(BrickType), config.Type);
        subtypeName.text = BrickConfigs.GetSubtypeName(config.Type, config.Subtype);
        description.text = config.Description;

        field.gameObject.SetActive(config.HasField);
        if (config.HasField) field.Init(config.FieldName, config.FieldType, data);

        objectSwitcher.gameObject.SetActive(config.HasObjectSelection);
        objectSwitcher.Init(data);
        slots.Init(config.Slots);

        deleteButton.onClick.AddListener(() =>
        {
            UINodeEditor.Instance.DeleteBrickNode(this);
        });
    }
}
