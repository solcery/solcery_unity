using UnityEngine;
using Solcery.FSM.Game;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Solcery.UI
{
    [RequireComponent(typeof(Button))]
    public class UIGameTransitionButton : MonoBehaviour
    {
        [SerializeField] private GameTransition transition = null;
        [SerializeField] protected Button button = null;

        protected virtual void OnEnable()
        {
            button.onClick.AddListener(() => { GameSM.Instance?.PerformTransition(transition); });
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
