using System;
using Sirenix.OdinInspector;

namespace Solcery.FSM
{
    public abstract class Parameter : SerializedScriptableObject
    {
        [NonSerialized] public Action OnPassed;

        public virtual void Subscribe() { }
    }
}
