using UnityEngine;

namespace Solcery.FSM
{
    public abstract class Bool : Parameter
    {
        [SerializeField] protected bool outcome;

        protected void SetOutcome(bool value)
        {
            if (outcome == value)
                OnPassed?.Invoke();
        }
    }
}
