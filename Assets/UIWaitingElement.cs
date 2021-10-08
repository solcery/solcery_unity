using TMPro;
using UnityEngine;

namespace Solcery
{
    public class UIWaitingElement : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] private TextMeshProUGUI text = null;
        [SerializeField] private string waitingFor = null;

        public void SetWaiting(bool isWaiting)
        {
            if (animator != null) animator.SetBool("isWaiting", isWaiting);
            if (text != null) text.text = isWaiting ? $"Waiting for {waitingFor}" : $"{waitingFor} received";
        }
    }
}
