namespace Solcery.FSM
{
    public abstract class Trigger : Parameter
    {
        public void Activate()
        {
            if (OnPassed != null)
            {
                OnPassed?.Invoke();
            }
        }
    }
}
