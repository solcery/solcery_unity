using UnityEngine;
using UnityEngine.UI;

namespace Grimmz.UI.Create.BrickEditor
{
    public class UISelectBrickButton : MonoBehaviour
    {
        public Transform Vert => verticalGroup;
        public BrickType BrickType { get; private set; }
        public UIBrick Parent { get; private set; }
        public int IndexInParentSlots { get; private set; }

        [SerializeField] private Transform verticalGroup = null;
        [SerializeField] private Button button = null;

        public void Init(BrickType brickType, Transform vert, UIBrick parent = null, int indexInParentSlots = 0)
        {
            BrickType = brickType;
            verticalGroup = vert;
            Parent = parent;
            IndexInParentSlots = indexInParentSlots;

            button.onClick.AddListener(() =>
            {
                UICreate.Instance.BrickEditor.OpenSubtypePopup(this);
            });
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
