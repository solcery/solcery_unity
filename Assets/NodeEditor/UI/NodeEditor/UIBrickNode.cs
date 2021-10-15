using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.NodeEditor
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
        [SerializeField] private UIBrickField field = null;
        [SerializeField] private UIBrickObjectSwitcher objectSwitcher = null;
        [SerializeField] private UIBrickSlots slots = null;
        [SerializeField] private UIBrickNodeHighlighter highligter = null;

        private Action<UIBrickNode> _onHighlighted, _onDeHighlighted;

        public void Init(BrickConfig config, BrickData data, UIBrickNode parent, int indexInParentSlots, Action<UIBrickNode> onHighlighted, Action<UIBrickNode> onDeHighlighted, Action brickInputChanged)
        {
            Debug.Log("BrickNode.Init");
            Debug.Log(brickInputChanged == null);
            highligter?.Init(() => { onHighlighted?.Invoke(this); }, () => { onDeHighlighted?.Invoke(this); });

            Config = config;
            Data = data;
            Parent = parent;
            IndexInParentSlots = indexInParentSlots;

            _onHighlighted = onHighlighted;
            _onDeHighlighted = onDeHighlighted;

            NodeSlots = new UINode[config.Slots.Count];
            Arrows = new UINodeArrow[config.Slots.Count];

            type.text = Enum.GetName(typeof(BrickType), config.Type);
            subtype.text = config.Name;
            description.text = config.Description;

            field.gameObject.SetActive(config.HasField);
            if (config.HasField) field.Init(config.FieldName, config.FieldType, data, brickInputChanged);

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

            cg.alpha = 1;
        }

        public override float GetMaxHeight()
        {
            BrickHeight = 15;

            type.transform.localPosition = new Vector2(type.transform.localPosition.x, -BrickHeight);
            BrickHeight += 20;

            BrickHeight += 5;
            subtype.transform.localPosition = new Vector2(subtype.transform.localPosition.x, -BrickHeight);
            BrickHeight += 20;


            if (Config.HasField)
            {
                BrickHeight += 5;
                field.transform.localPosition = new Vector2(field.transform.localPosition.x, -BrickHeight);
                BrickHeight += 30;
            }

            if (Config.HasObjectSelection)
            {
                BrickHeight += 5;
                objectSwitcher.transform.localPosition = new Vector2(objectSwitcher.transform.localPosition.x, -BrickHeight);
                BrickHeight += 40;
            }

            if (Config.Slots.Count > 0)
            {
                BrickHeight += 5;
                slots.transform.localPosition = new Vector2(slots.transform.localPosition.x, -BrickHeight);
                BrickHeight += 50;
            }
            else
            {
                BrickHeight += 15;
            }

            return base.GetMaxHeight();
        }

        public override float GetMaxWidth()
        {
            BrickWidth = Mathf.Max(200, slots.Slots.Count * 80);

            return base.GetMaxWidth();
        }

        public override void Rebuild()
        {
            contents.sizeDelta = new Vector2(BrickWidth, BrickHeight);
            base.Rebuild();
        }
    }
}
