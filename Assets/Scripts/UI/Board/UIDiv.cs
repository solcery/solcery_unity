using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIDiv : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] private TextMeshProUGUI hpDivText = null;

        public void Show(int div)
        {
            if (animator == null) return;
            if (hpDivText == null) return;

            var divString = Mathf.Abs(div).ToString();
            hpDivText.text = div > 0 ? $"+ {divString}" : $"- {divString}";
            animator.SetTrigger(div > 0 ? "Increased" : "Decreased");
        }
    }
}
