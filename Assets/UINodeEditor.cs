using Solcery;
using Solcery.UI.Create.BrickEditor;
using Solcery.Utils;
using UnityEngine;

public class UINodeEditor : Singleton<UINodeEditor>
{
    public GameObject BrickNodePrefab;
    public GameObject SelectBrickNode;
    public UINode Genesis;
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private GameObject contentBlocker = null;
    [SerializeField] private UIBrickSubtypePopup subtypePopup = null;

    private BrickTree _brickTree;

    public void Rebuild()
    {
        var maxWidth = Genesis.GetMaxWidth();
        var maxHeight = Genesis.GetMaxHeight();

        rect.sizeDelta = new Vector2(maxWidth, maxHeight);
        Genesis.Rebuild();
    }

    public void OpenSubtypePopup(UISelectBrickNode button)
    {
        contentBlocker.SetActive(true);
        contentBlocker.transform.SetAsLastSibling();
        subtypePopup.gameObject.SetActive(true);
        subtypePopup.Open(button, OnBrickAdded);
    }

    private void OnBrickAdded(SubtypeNameConfig subtypeNameConfig, UISelectBrickNode button)
    {
        contentBlocker.gameObject.SetActive(false);
        subtypePopup.Close();
        CreateBrickNode(subtypeNameConfig.Config, button);
    }

    void Start()
    {
        _brickTree = new BrickTree();
        CreateFirstButton();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Rebuild();
    }

    private void CreateFirstButton()
    {
        var selectBrickNode = Instantiate(SelectBrickNode, rect).GetComponent<UISelectBrickNode>();
        selectBrickNode.Init(BrickType.Action, transform);
        Genesis = selectBrickNode;
        Rebuild();
    }

    private void CreateBrickNode(BrickConfig config, UISelectBrickNode button)
    {
        DestroyImmediate(button.gameObject);
        var brickData = new BrickData(config);
        var brickNode = Instantiate(BrickNodePrefab, button.ParentTransform).GetComponent<UIBrickNode>();
        brickNode.Init(config, brickData, button.Parent, button.IndexInParentSlots);

        if (button.Parent == null)
        {
            _brickTree.SetGenesis(brickData);
            Genesis = brickNode;
        }
        else
        {
            button.Parent.Slots[button.IndexInParentSlots] = brickNode;
            button.Parent.Data.Slots[button.IndexInParentSlots] = brickData;
            //TODO: set BrickSlots for UIBrickNode
            // button.Parent.Slots.Slots[button.IndexInParentSlots].SetFilled(true);
        }

        for (int i = 0; i < config.Slots.Count; i++)
        {
            var selectBrickButton = Instantiate(SelectBrickNode, brickNode.transform).GetComponent<UISelectBrickNode>();
            selectBrickButton.Init(config.Slots[i].Type, brickNode.transform, brickNode, i);
            brickNode.Slots[i] = selectBrickButton;
        }

        Rebuild();
    }
}
