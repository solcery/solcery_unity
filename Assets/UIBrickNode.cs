using Solcery;
using UnityEngine;
using UnityEngine.UI;

public class UIBrickNode : UINode
{
    public BrickConfig Config { get; private set; }
    public BrickData Data { get; private set; }
    public UIBrickNode Parent { get; private set; }
    public int IndexInParentSlots { get; private set; }

    [SerializeField] private Button deleteButton = null;

    public void Init(BrickConfig config, BrickData data, UIBrickNode parent, int indexInParentSlots)
    {
        Config = config;
        Data = data;
        Parent = parent;
        IndexInParentSlots = indexInParentSlots;

        Slots = new UINode[config.Slots.Count];
        Arrows = new TestArrow[config.Slots.Count];

        deleteButton.onClick.AddListener(() =>
        {
            UINodeEditor.Instance.DeleteBrickNode(this);
        });
    }
}
