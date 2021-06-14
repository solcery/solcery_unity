using System;
using Cysharp.Threading.Tasks;
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

    [SerializeField] private CanvasGroup cg = null;
    [SerializeField] private RectTransform contents = null;
    [SerializeField] private Button deleteButton = null;
    [SerializeField] private TextMeshProUGUI type = null;
    [SerializeField] private RectTransform typeRect = null;
    [SerializeField] private TextMeshProUGUI subtype = null;
    [SerializeField] private RectTransform subtypeRect = null;
    [SerializeField] private TextMeshProUGUI description = null;
    [SerializeField] private RectTransform descriptionRect = null;
    [SerializeField] private LayoutElement descriptionLE = null;
    [SerializeField] private UIBrickField field = null;
    [SerializeField] private RectTransform fieldRect = null;
    [SerializeField] private UIBrickObjectSwitcher objectSwitcher = null;
    [SerializeField] private RectTransform objectSwitcherRect = null;
    [SerializeField] private UIBrickSlots slots = null;
    [SerializeField] private RectTransform slotsRect = null;

    public async UniTask Init(BrickConfig config, BrickData data, UIBrickNode parent, int indexInParentSlots)
    {
        Config = config;
        Data = data;
        Parent = parent;
        IndexInParentSlots = indexInParentSlots;

        NodeSlots = new UINode[config.Slots.Count];
        Arrows = new TestArrow[config.Slots.Count];

        type.text = Enum.GetName(typeof(BrickType), config.Type);
        subtype.text = BrickConfigs.GetSubtypeName(config.Type, config.Subtype);
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

        LayoutRebuilder.ForceRebuildLayoutImmediate(contents);
        BrickWidth = contents.sizeDelta.x;
        BrickHeight = contents.sizeDelta.y;
        descriptionLE.preferredWidth = BrickWidth - 24;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contents);
        await UniTask.WaitForEndOfFrame();
        BrickWidth = contents.sizeDelta.x;
        BrickHeight = contents.sizeDelta.y;
        cg.alpha = 1;
        Debug.Log(BrickHeight);
    }

    public override async UniTask<float> GetMaxHeight()
    {
        // LayoutRebuilder.ForceRebuildLayoutImmediate(contents);
        BrickWidth = contents.sizeDelta.x;
        BrickHeight = contents.sizeDelta.y;
        descriptionLE.preferredWidth = BrickWidth - 24;
        Debug.Log(BrickHeight);

        return await base.GetMaxHeight();
    }

    public override async UniTask<float> GetMaxWidth()
    {
        // LayoutRebuilder.ForceRebuildLayoutImmediate(contents);
        BrickWidth = contents.sizeDelta.x;
        BrickHeight = contents.sizeDelta.y;
        descriptionLE.preferredWidth = BrickWidth - 24;
        Debug.Log(BrickHeight);

        return await base.GetMaxWidth();
    }

    public override async UniTask Rebuild()
    {
        // var slotsWidth = Config.Slots.Count * 80;
        // BrickWidth = Mathf.Max(BrickWidth, slotsWidth);

        // LayoutRebuilder.ForceRebuildLayoutImmediate(contents);
        // BrickWidth = contents.sizeDelta.x;
        // BrickHeight = contents.sizeDelta.y;
        // descriptionLE.preferredWidth = BrickWidth;
        // Debug.Log(BrickWidth);

        // Debug.Log(description.renderedHeight);
        // Debug.Log(descriptionRect.sizeDelta.y);
        // description.ForceMeshUpdate();
        // Debug.Log(description.renderedHeight);
        // Debug.Log(descriptionRect.sizeDelta.y);

        // contents.sizeDelta = new Vector2(0, BrickWidth);
        // typeRect.sizeDelta = new Vector2(20, BrickWidth);
        // subtypeRect.sizeDelta = new Vector2(20, BrickWidth);
        // descriptionRect.sizeDelta = new Vector2(0, BrickWidth);
        // description.ForceMeshUpdate();
        // Debug.Log(descriptionRect.sizeDelta.y);
        // Debug.Log(description.preferredHeight);


        // BrickHeight = 0f;
        // BrickHeight += typeRect.sizeDelta.y; //type
        // BrickHeight += subtypeRect.sizeDelta.y; //subtype
        // // LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionRect);
        // // await UniTask.Delay(10);
        // BrickHeight += descriptionRect.sizeDelta.y; //description

        // if (Config.HasField || Config.HasObjectSelection)
        // {
        //     field.transform.localPosition = new Vector2(field.transform.localPosition.x, -BrickHeight);
        //     objectSwitcher.transform.localPosition = new Vector2(objectSwitcher.transform.localPosition.x, -BrickHeight);
        //     BrickHeight += 40f;
        // }

        // slotsRect.sizeDelta = new Vector2(200, 35);
        // slotsRect.localPosition = new Vector2(0, -BrickHeight);

        // if (Config.Slots.Count > 0)
        //     BrickHeight += 35f;

        // contents.sizeDelta = new Vector2(BrickWidth, BrickHeight);
        await base.Rebuild();
    }
}
