using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
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
        [SerializeField] private TextMeshProUGUI subtype = null;
        [SerializeField] private TextMeshProUGUI description = null;
        [SerializeField] private LayoutElement descriptionLE = null;
        [SerializeField] private UIBrickField field = null;
        [SerializeField] private UIBrickObjectSwitcher objectSwitcher = null;
        [SerializeField] private UIBrickSlots slots = null;

        public async UniTask Init(BrickConfig config, BrickData data, UIBrickNode parent, int indexInParentSlots)
        {
            Config = config;
            Data = data;
            Parent = parent;
            IndexInParentSlots = indexInParentSlots;

            NodeSlots = new UINode[config.Slots.Count];
            Arrows = new UINodeArrow[config.Slots.Count];

            type.text = Enum.GetName(typeof(BrickType), config.Type);
            subtype.text = BrickConfigs.GetSubtypeName(config.Type, config.Subtype);
            description.text = config.Description;

            field.gameObject.SetActive(config.HasField);
            if (config.HasField) field.Init(config.FieldName, config.FieldType, data);

            objectSwitcher.gameObject.SetActive(config.HasObjectSelection);
            objectSwitcher.Init(data);

            if (config.Slots.Count > 0)
            {
                slots.gameObject.SetActive(true);
                slots.Init(config.Slots);
            }
            else
            {
                slots.gameObject.SetActive(false);
            }


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
        }

        public override float GetMaxHeight()
        {
            BrickWidth = contents.sizeDelta.x;
            BrickHeight = contents.sizeDelta.y;
            descriptionLE.preferredWidth = BrickWidth - 24;

            return base.GetMaxHeight();
        }

        public override float GetMaxWidth()
        {
            BrickWidth = contents.sizeDelta.x;
            BrickHeight = contents.sizeDelta.y;
            descriptionLE.preferredWidth = BrickWidth - 24;

            return base.GetMaxWidth();
        }
    }
}
