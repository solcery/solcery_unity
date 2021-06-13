using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickSlot : MonoBehaviour
    {
        [SerializeField] private Toggle toggle = null;
        [SerializeField] private TextMeshProUGUI slotName = null;

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
