using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIDiff : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] private TextMeshProUGUI hpDiffText = null;

        public void Show(int diff)
        {
            if (animator == null) return;
            if (hpDiffText == null) return;

            var diffString = Mathf.Abs(diff).ToString();
            hpDiffText.text = diff > 0 ? $"+ {diffString}" : $"- {diffString}";
            animator.SetTrigger(diff > 0 ? "Increased" : "Decreased");
        }
    }
}
