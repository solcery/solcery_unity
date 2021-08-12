using System;
using Sirenix.OdinInspector;

namespace Solcery.FSM
{
    public abstract class Trigger : SerializedScriptableObject
    {
        [NonSerialized] public Action OnActivated;

        public void Activate()
        {
            if (OnActivated != null)
            {
                OnActivated?.Invoke();
            }
        }
    }
}
