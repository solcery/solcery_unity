using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.NodeEditor
{
    public class UINodeEditorInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsMouseOver { get; private set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsMouseOver = false;
        }
    }
}
