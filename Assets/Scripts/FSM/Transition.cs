using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery.FSM
{
    public abstract class Transition<TTransition, TState, TTrigger> : SerializedScriptableObject
    where TTransition : Transition<TTransition, TState, TTrigger>
    where TState : State<TState, TTrigger, TTransition>
    where TTrigger : Trigger
    {
        [SerializeField] private TState from;
        [SerializeField] private TState to;

        public TState From => from;
        public TState To => to;

#pragma warning disable 1998
        public virtual async UniTask PerformTransition() { }
#pragma warning restore 1998
    }
}