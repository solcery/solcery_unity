using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery.FSM
{
    public abstract class State<TState, TTrigger, TTransition> : SerializedScriptableObject
    where TState : State<TState, TTrigger, TTransition>
    where TTrigger : Trigger
    where TTransition : Transition<TTransition, TState, TTrigger>
    {
        public Dictionary<TTrigger, TTransition> Transitions => transitions;
        [SerializeField] private Dictionary<TTrigger, TTransition> transitions = new Dictionary<TTrigger, TTransition>();

        public abstract UniTask Enter();
        public abstract UniTask Exit();

        public virtual void PerformUpdate() { }
    }
}