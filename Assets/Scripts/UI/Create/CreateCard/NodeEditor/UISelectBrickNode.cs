using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI.NodeEditor
{
    public class UISelectBrickNode : UINode, IPointerEnterHandler, IPointerExitHandler
    {
        public BrickType BrickType { get; private set; }
        public UIBrickNode Parent { get; private set; }
        public Transform ParentTransform { get; private set; }
        public int IndexInParentSlots { get; private set; }
        public RectTransform Center => center;

        [SerializeField] private RectTransform center = null;
        [SerializeField] private Button button = null;
        [SerializeField] private Image buttonImage = null;
        [SerializeField] private Sprite activeButtonSprite = null;
        [SerializeField] private Sprite inactiveButtonSprite = null;

        private UIBrickSlot _slot;

        public void Init(BrickType brickType, Transform parentTransform, UIBrickNode parent = null, int indexInParentSlots = 0, UIBrickSlot slot = null)
        {
            BrickType = brickType;
            ParentTransform = parentTransform;
            Parent = parent;
            IndexInParentSlots = indexInParentSlots;
            _slot = slot;

            button.onClick.AddListener(() =>
            {
                UINodeEditor.Instance.OpenSubtypePopup(this);
            });
        }

        public void DeInit()
        {
            _slot = null;
            button.onClick.RemoveAllListeners();
        }

        public void SetActive(bool isActive)
        {
            buttonImage.sprite = isActive ? activeButtonSprite : inactiveButtonSprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _slot?.SetHighlighted(true);
            SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _slot?.SetHighlighted(false);
            SetActive(false);
        }
    }
}