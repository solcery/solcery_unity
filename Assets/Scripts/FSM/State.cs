using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery.FSM
{
    public abstract class State<TState, TParameter, TTransition> : SerializedScriptableObject
    where TState : State<TState, TParameter, TTransition>
    where TParameter : Parameter
    where TTransition : Transition<TTransition, TState, TParameter>
    {
        public Dictionary<TTransition, TParameter> Transitions => transitions;
        [SerializeField] private Dictionary<TTransition, TParameter> transitions;

        private List<ParamAction> _paramSubscriptions;
        private Action<TTransition> _performTransition;

        public virtual async UniTask Enter(Action<TTransition> performTransition)
        {
            Debug.Log($"enter {this.name}");
            
            _performTransition = performTransition;

            if (transitions == null || transitions.Count <= 0)
                return;

            _paramSubscriptions = new List<ParamAction>();

            foreach (var transitionParam in transitions)
            {
                var transition = transitionParam.Key;
                var param = transitionParam.Value;

                if (param == null || transition == null)
                    return;

                Action onParamAction = () =>
                {
                    Debug.Log($"{transition.name}");
                    _performTransition.Invoke(transition);
                };

                _paramSubscriptions.Add(new ParamAction(param, onParamAction));
                param.OnPassed += onParamAction;
                param.Subscribe();
            }
        }

        public virtual async UniTask Exit()
        {
            Debug.Log($"exit {this.name}");
            if (_paramSubscriptions == null || _paramSubscriptions.Count <= 0)
                return;

            Debug.Log(_paramSubscriptions.Count);
            foreach (var paramAction in _paramSubscriptions)
            {
                var param = paramAction.Param;
                var onParamAction = paramAction.Action;

                if (param == null || onParamAction == null)
                    continue;

                param.OnPassed -= onParamAction;
            }

            _paramSubscriptions = null;
        }

        public virtual void PerformUpdate() { }

        private class ParamAction
        {
            public TParameter Param;
            public Action Action;

            public ParamAction(TParameter param, Action action)
            {
                Param = param;
                Action = action;
            }
        }
    }
}