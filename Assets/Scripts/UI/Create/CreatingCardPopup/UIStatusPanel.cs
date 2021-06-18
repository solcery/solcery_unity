using TMPro;
using UnityEngine;

namespace Solcery.UI
{
    public class UIStatusPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statusText = null;
        [SerializeField] private string waitingText = null;
        [SerializeField] private string successText = null;
        [SerializeField] private string failText = null;
        [SerializeField] private GameObject waiting = null;
        [SerializeField] private GameObject success = null;
        [SerializeField] private GameObject fail = null;

        protected UIStatusState _currentState;

        public void SetState(UIStatusState newState)
        {
            ExitState(_currentState);
            EnterState(newState);
            _currentState = newState;
        }

        protected void ExitState(UIStatusState exitingState)
        {
            switch (exitingState)
            {
                case UIStatusState.Waiting:
                    waiting?.SetActive(false);
                    break;
                case UIStatusState.Fail:
                    fail?.SetActive(false);
                    break;
                case UIStatusState.Success:
                    success?.SetActive(false);
                    break;
            }
        }

        protected void EnterState(UIStatusState enteringState)
        {
            switch (enteringState)
            {
                case UIStatusState.Waiting:
                    waiting?.SetActive(true);
                    if (statusText != null) statusText.text = waitingText;
                    break;
                case UIStatusState.Fail:
                    fail?.SetActive(true);
                    if (statusText != null) statusText.text = failText;
                    break;
                case UIStatusState.Success:
                    success?.SetActive(true);
                    if (statusText != null) statusText.text = successText;
                    break;
            }
        }
    }

    public enum UIStatusState
    {
        Waiting,
        Success,
        Fail
    }
}
