using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.FSM
{
    public class FullSM<TSM, TState, TTransition, TParameter> : UpdateableSingleton<TSM>
    where TSM : FullSM<TSM, TState, TTransition, TParameter>
    where TState : State<TState, TParameter, TTransition>
    where TTransition : Transition<TTransition, TState, TParameter>
    where TParameter : Parameter
    {
        [SerializeField] private TState _entryState = null;

        private TState _currentState;

        public async UniTask Enter()
        {
            if (_entryState != null)
            {
                _currentState = _entryState;
                await _currentState.Enter(StateCallback);
            }
        }

        private async UniTask<bool> PerformTransition(TTransition transition)
        {
            if (_currentState == null)
                return false;

            if (transition.To == null)
                return false;

            await _currentState.Exit();
            await transition.PerformTransition();
            _currentState = transition.To;
            await _currentState.Enter(StateCallback);

            return true;
        }

        private void StateCallback(TTransition transition)
        {
            PerformTransition(transition).Forget();
        }

        public override void PerformUpdate()
        {
            _currentState?.PerformUpdate();
        }

        public async UniTask Exit()
        {
            if (_currentState != null)
                await _currentState.Exit();
        }
    }
}