using Cysharp.Threading.Tasks;
using Solcery;
using Solcery.UI.Create.BrickEditor;
using Solcery.Utils;
using TMPro;
using UnityEngine;

public class UINodeEditor : Singleton<UINodeEditor>
{
    public BrickTree BrickTree => _brickTree;
    public GameObject BrickNodePrefab;
    public GameObject SelectBrickNode;
    public UINode Genesis;
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private GameObject contentBlocker = null;
    [SerializeField] private UIBrickSubtypePopup subtypePopup = null;
    [SerializeField] private TextMeshProUGUI helperText = null;

    private BrickTree _brickTree;

    public void Init()
    {
        _brickTree = new BrickTree();
        CreateFirstButton();
    }

    public void DeInit()
    {
        _brickTree = null;
    }

    public async UniTask Rebuild()
    {
        _brickTree?.CheckValidity();

        var maxWidth = await Genesis.GetMaxWidth();
        var maxHeight = await Genesis.GetMaxHeight();

        rect.sizeDelta = new Vector2(maxWidth, maxHeight + 400);
        Genesis.Rebuild().Forget();
    }

    public void OpenSubtypePopup(UISelectBrickNode button)
    {
        helperText.gameObject.SetActive(false);
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

    private async UniTaskVoid CreateBrickNode(BrickConfig config, UISelectBrickNode button)
    {
        DestroyImmediate(button.gameObject);
        var brickData = new BrickData(config);
        var brickNode = Instantiate(BrickNodePrefab, button.ParentTransform).GetComponent<UIBrickNode>();
        await brickNode.Init(config, brickData, button.Parent, button.IndexInParentSlots);

        if (button.Parent == null)
        {
            _brickTree.SetGenesis(brickData);
            Genesis = brickNode;
        }
        else
        {
            button.Parent.NodeSlots[button.IndexInParentSlots] = brickNode;
            button.Parent.Data.Slots[button.IndexInParentSlots] = brickData;
            //TODO: set BrickSlots for UIBrickNode
            button.Parent.Slots.Slots[button.IndexInParentSlots].SetFilled(true);
        }

        for (int i = 0; i < config.Slots.Count; i++)
        {
            var selectBrickButton = Instantiate(SelectBrickNode, brickNode.transform).GetComponent<UISelectBrickNode>();
            selectBrickButton.Init(config.Slots[i].Type, brickNode.transform, brickNode, i);
            brickNode.NodeSlots[i] = selectBrickButton;
        }

        await Rebuild();
    }

    public void DeleteBrickNode(UIBrickNode brickNode)
    {
        if (brickNode.Parent == null)
        {
            var selectBrickButton = Instantiate(SelectBrickNode, transform).GetComponent<UISelectBrickNode>();
            Genesis = selectBrickButton;
            _brickTree.SetGenesis(null);
            selectBrickButton.Init(BrickType.Action, transform);
            helperText.gameObject.SetActive(true);
        }
        else
        {
            var selectBrickButton = Instantiate(SelectBrickNode, brickNode.Parent.transform).GetComponent<UISelectBrickNode>();
            brickNode.Parent.Data.Slots[brickNode.IndexInParentSlots] = null;
            //TODO: set BrickSlots for UIBrickNode
            brickNode.Parent.Slots.Slots[brickNode.IndexInParentSlots].SetFilled(false);
            selectBrickButton.Init(brickNode.Config.Type, brickNode.Parent.transform, brickNode.Parent, brickNode.IndexInParentSlots);
            brickNode.Parent.NodeSlots[brickNode.IndexInParentSlots] = selectBrickButton;
        }

        DestroyImmediate(brickNode.gameObject);
        Rebuild();
    }

    public void DeleteGenesisBrickNode()
    {
        if (Genesis is UIBrickNode)
            DeleteBrickNode(Genesis as UIBrickNode);
    }
}
