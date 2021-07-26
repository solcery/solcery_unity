using UnityEngine;
using Solcery.FSM.Game;
using UnityEngine.UI;

namespace Solcery.UI
{
    [RequireComponent(typeof(Button))]
    public class UIDappTransitionButton : MonoBehaviour
    {
        [SerializeField] private DappTransition transition = null;
        [SerializeField] protected Button button = null;

        protected virtual void OnEnable()
        {
            button.onClick.AddListener(() => { DappSM.Instance?.PerformTransition(transition); });
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
