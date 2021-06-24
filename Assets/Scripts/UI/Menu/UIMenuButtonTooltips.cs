using TMPro;
using UnityEngine;

namespace Solcery.UI.Menu
{
    public class UIMenuButtonTooltips : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tooltipText = null;

        public void SetText(string text)
        {
            if (tooltipText != null)
            {
                tooltipText.gameObject.SetActive(true);
                tooltipText.text = text;
            }
        }

        public void Disable()
        {
            if (tooltipText != null)
                tooltipText.gameObject.SetActive(false);
        }
    }
}
