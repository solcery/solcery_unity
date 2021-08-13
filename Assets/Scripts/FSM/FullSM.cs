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
        private Dictionary<TParameter, Action> _triggerSubscriptions;

        public async UniTask Enter()
        {
            if (_entryState != null)
            {
                _currentState = _entryState;
                SubscribeToStateTriggers();
                await _currentState.Enter();
            }
        }

        private async UniTask<bool> PerformTransition(TTransition transition)
        {
            if (_currentState == null)
                return false;

            if (transition.To == null)
                return false;

            UnsubscribeFromStateTriggers();
            await _currentState.Exit();
            await transition.PerformTransition();
            _currentState = transition.To;
            await _currentState.Enter();
            SubscribeToStateTriggers();

            return true;
        }

        private void SubscribeToStateTriggers()
        {
            if (_currentState == null)
                return;

            if (_currentState.Transitions == null || _currentState.Transitions.Count <= 0)
                return;

            _triggerSubscriptions = new Dictionary<TParameter, Action>();

            foreach (var triggerTransition in _currentState.Transitions)
            {
                var trigger = triggerTransition.Key;
                var transition = triggerTransition.Value;

                if (trigger == null || transition == null)
                    continue;

                var onTriggerAction = UniTask.Action(async () =>
                {
                    await PerformTransition(transition);
                });
                trigger.OnPassed += onTriggerAction;
                _triggerSubscriptions.Add(trigger, onTriggerAction);
            }
        }

        private void UnsubscribeFromStateTriggers()
        {
            if (_triggerSubscriptions == null)
                return;

            if (_triggerSubscriptions.Count <= 0)
                return;

            foreach (var triggerAction in _triggerSubscriptions)
            {
                var trigger = triggerAction.Key;
                var onTriggerAction = triggerAction.Value;

                if (trigger == null || onTriggerAction == null)
                    continue;

                trigger.OnPassed -= onTriggerAction;
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