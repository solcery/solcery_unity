using Solcery;

public class UIBrickNode : UINode
{
    public BrickConfig Config { get; private set; }
    public BrickData Data { get; private set; }
    public UINode Parent { get; private set; }
    public int IndexInParentSlots { get; private set; }

    public void Init(BrickConfig config, BrickData data, UINode parent, int indexInParentSlots)
    {
        Config = config;
        Data = data;
        Parent = parent;
        IndexInParentSlots = indexInParentSlots;

        Slots = new UINode[config.Slots.Count];
        Arrows = new TestArrow[config.Slots.Count];
    }
}
