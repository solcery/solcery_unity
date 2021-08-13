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
        public Dictionary<TParameter, TTransition> Transitions => transitions;
        [SerializeField] private Dictionary<TParameter, TTransition> transitions = new Dictionary<TParameter, TTransition>();

        public abstract UniTask Enter();
        public abstract UniTask Exit();

        public virtual void PerformUpdate() { }
    }
}