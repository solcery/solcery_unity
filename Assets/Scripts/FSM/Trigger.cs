namespace Solcery.FSM
{
    public abstract class Trigger : Parameter
    {
        // [NonSerialized] public Action OnActivated;

        public void Activate()
        {
            if (OnPassed != null)
            {
                OnPassed?.Invoke();
            }
        }
    }
}
