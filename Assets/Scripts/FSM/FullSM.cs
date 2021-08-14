using System;
using System.Collections.Generic;
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
        private Dictionary<TParameter, Action> _paramSubscriptions;

        public async UniTask Enter()
        {
            if (_entryState != null)
            {
                _currentState = _entryState;
                SubscribeToStateParams();
                await _currentState.Enter();
            }
        }

        private async UniTask<bool> PerformTransition(TTransition transition)
        {
            if (_currentState == null)
                return false;

            if (transition.To == null)
                return false;

            UnsubscribeFromStateParams();
            await _currentState.Exit();
            await transition.PerformTransition();
            _currentState = transition.To;
            await _currentState.Enter();
            SubscribeToStateParams();

            return true;
        }

        private void SubscribeToStateParams()
        {
            if (_currentState == null)
                return;

            if (_currentState.Transitions == null || _currentState.Transitions.Count <= 0)
                return;

            _paramSubscriptions = new Dictionary<TParameter, Action>();

            foreach (var paramTransition in _currentState.Transitions)
            {
                var param = paramTransition.Key;
                var transition = paramTransition.Value;

                if (param == null || transition == null)
                    continue;

                var onParamAction = UniTask.Action(async () =>
                {
                    await PerformTransition(transition);
                });

                param.OnPassed += onParamAction;
                param.StartTracking();
                _paramSubscriptions.Add(param, onParamAction);
            }
        }

        private void SubscribeActionToParam()
        {

        }

        private void UnsubscribeFromStateParams()
        {
            if (_paramSubscriptions == null)
                return;

            if (_paramSubscriptions.Count <= 0)
                return;

            foreach (var paramAction in _paramSubscriptions)
            {
                var param = paramAction.Key;
                var onParamAction = paramAction.Value;

                if (param == null || onParamAction == null)
                    continue;

                param.OnPassed -= onParamAction;
            }
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