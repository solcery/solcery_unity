using UnityEngine;
using UnityEngine.UI;
using Solcery.FSM.Create;

namespace Solcery.UI.Create
{
    [RequireComponent(typeof(Button))]
    public class UICreateTransitionButton : MonoBehaviour
    {
        [SerializeField] private CreateTransition transition = null;
        [SerializeField] protected Button button = null;

        protected virtual void OnButtonClicked()
        {
            CreateSM.Instance?.PerformTransition(transition);
        }

        void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
