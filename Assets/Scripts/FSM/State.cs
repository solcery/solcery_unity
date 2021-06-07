using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Grimmz.FSM
{
    public abstract class State : SerializedScriptableObject
    {
#pragma warning disable 1998
        public virtual async UniTask Enter() { }
        public virtual async UniTask Exit() { }
#pragma warning restore 1998

        public virtual void PerformUpdate() { }
    }
}