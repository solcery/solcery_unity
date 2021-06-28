using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UIBrickNodeHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image highlighter = null;

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetActive(false);
        }

        private void SetActive(bool isActive)
        {
            highlighter.gameObject.SetActive(isActive);
        }
    }
}
