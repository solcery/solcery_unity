using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI.NodeEditor
{
    public class UIBrickSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Toggle toggle = null;
        [SerializeField] private TextMeshProUGUI slotName = null;
        [SerializeField] private Color normalTextColor;
        [SerializeField] private Color highlightedTextColor;

        private UISelectBrickNode _selectBrickNode = null;

        public void Init(string name)
        {
            slotName.text = name;
        }

        public void SetFilled(bool isFilled)
        {
            toggle.isOn = isFilled;
        }

        public void SetButton(UISelectBrickNode selectBrickNode)
        {
            _selectBrickNode = selectBrickNode;
        }

        public void SetHighlighted(bool isSelected)
        {
            slotName.color = isSelected ? highlightedTextColor : normalTextColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // slotName.color = highlightedTextColor;
            // _selectBrickNode?.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // slotName.color = normalTextColor;
            // _selectBrickNode?.SetActive(false);
        }
    }
}
