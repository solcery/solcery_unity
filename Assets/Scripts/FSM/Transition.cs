using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery.FSM
{
    public abstract class Transition<TState> : SerializedScriptableObject
    where TState : State
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