using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Solcery.FSM
{
    public abstract class State<TState, TTrigger, TTransition> : SerializedScriptableObject
    where TState : State<TState, TTrigger, TTransition>
    where TTrigger : Trigger
    where TTransition : Transition<TTransition, TState, TTrigger>
    {
        public Dictionary<TTrigger, TTransition> Transitions;

        public abstract UniTask Enter();
        public abstract UniTask Exit();

        public virtual void PerformUpdate() { }
    }
}