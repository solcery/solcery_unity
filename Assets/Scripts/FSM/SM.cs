using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.FSM
{
    public class SM<TSM, TState, TTransition> : UpdateableSingleton<TSM>
    where TSM : SM<TSM, TState, TTransition>
    where TState : State
    where TTransition : Transition<TState>
    {
        [SerializeField] private TTransition _initialTransition = null;
        private TState _currentState;

        public async UniTask PerformInitialTransition()
        {
            if (_initialTransition != null)
                await PerformTransition(_initialTransition);
        }

        public async UniTask<bool> PerformTransition(TTransition transition)
        {
            if (_currentState != null)
            {
                if (transition.From != null)
                {
                    if (_currentState != transition.From)
                    {
                        Debug.LogError("Current state is different from transition.From!");
                        return false;
                    }
                }
                else
                    await _currentState.Exit();
            }

            await transition.PerformTransition();

            if (transition.To == null)
            {
                Debug.LogError("Transition.To is null!");
                return false;
            }
            else
            {
                _currentState = transition.To;
                await transition.To.Enter();
            }

            return true;
        }

        public override void PerformUpdate()
        {
            _currentState?.PerformUpdate();
        }
    }
}
