using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.FSM
{
    public class FullSM<TSM, TState, TTransition, TTrigger> : UpdateableSingleton<TSM>
    where TSM : FullSM<TSM, TState, TTransition, TTrigger>
    where TState : State<TState, TTrigger, TTransition>
    where TTransition : Transition<TTransition, TState, TTrigger>
    where TTrigger : Trigger
    {
        [SerializeField] private bool hasInitialTransition;
        [ShowIf("hasInitialTransition")] [SerializeField] private TTransition _initialTransition = null;
        private TState _currentState;

        public async UniTask PerformInitialTransition()
        {
            if (hasInitialTransition && _initialTransition != null)
                await PerformTransition(_initialTransition);
        }

        private async UniTask<bool> PerformTransition(TTransition transition)
        {
            if (_currentState == null)
                return false;

            if (transition.From == null)
                return false;

            if (transition.To == null)
                return false;

            if (_currentState != transition.From)
                return false;

            await _currentState.Exit();
            await transition.PerformTransition();
            _currentState = transition.To;
            await _currentState.Enter();

            return true;
        }

        public override void PerformUpdate()
        {
            _currentState?.PerformUpdate();
        }

        public async UniTask Finish()
        {
            if (_currentState != null)
                await _currentState.Exit();
        }
    }
}