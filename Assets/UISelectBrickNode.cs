using Solcery;
using UnityEngine;
using UnityEngine.UI;

public class UISelectBrickNode : UINode
{
    public BrickType BrickType { get; private set; }
    public UIBrickNode Parent { get; private set; }
    public Transform ParentTransform { get; private set; }
    public int IndexInParentSlots { get; private set; }

    [SerializeField] private Button button = null;

    public void Init(BrickType brickType, Transform parentTransform, UIBrickNode parent = null, int indexInParentSlots = 0)
    {
        BrickType = brickType;
        ParentTransform = parentTransform;
        Parent = parent;
        IndexInParentSlots = indexInParentSlots;

        button.onClick.AddListener(() =>
        {
            UINodeEditor.Instance.OpenSubtypePopup(this);
        });
    }

    public void DeInit()
    {
        button.onClick.RemoveAllListeners();
    }
}
