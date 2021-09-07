using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 1998
namespace Ilumisoft.VisualStateMachine
{
    public abstract class TransitionBehaviour : MonoBehaviour
    {
        public StateMachine StateMachine => stateMachine;
        public abstract string TransitionId { get; }

        [SerializeField] protected StateMachine stateMachine;

        Transition _transition;
        CancellationTokenSource _transitionCTS;

        protected virtual async UniTask Awake()
        {
            if (StateMachine != null)
            {
                _transition = stateMachine.Graph.GetTransition(TransitionId);

                if (_transition != null)
                {
                    _transition.behaviour = this;
                    // _transition.EnterTransition = UniTask.Delay(System.TimeSpan.FromSeconds(3f));
                    // _transition.ExitTransition = ExitTransition();
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (StateMachine != null && _transition != null)
            {
                // _transition.EnterTransition = UniTask.Yield(_transitionCTS.Token);
                // _transition.ExitTransition = UniTask.Yield(_transitionCTS.Token);
            }

            _transitionCTS?.Dispose();
        }

        public virtual async UniTask EnterTransition()
        {
            if (_transitionCTS != null)
                _transitionCTS?.Dispose();

            _transitionCTS = new CancellationTokenSource();
        }

        public virtual async UniTask ExitTransition()
        {
            _transitionCTS?.Cancel();
            _transitionCTS?.Dispose();
        }
    }
}
#pragma warning restore 1998
