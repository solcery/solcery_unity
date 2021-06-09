using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickSlot : MonoBehaviour
    {
        public UILineRenderer LineRenderer => lineRenderer;
        public RectTransform LinePointTop => linePointTop;
        public RectTransform LinePointBot => linePointBot;

        [SerializeField] private Toggle toggle = null;
        [SerializeField] private TextMeshProUGUI slotName = null;

        [SerializeField] private UILineRenderer lineRenderer = null;

        [SerializeField] private RectTransform linePointTop = null;
        [SerializeField] private RectTransform linePointBot = null;

        public void Init(string name)
        {
            slotName.text = name;
        }

        public void SetFilled(bool isFilled)
        {
            toggle.isOn = isFilled;
        }
    }
}
