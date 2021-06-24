namespace Solcery.Utils
{
    public abstract class UpdateableSingleton<T> : UpdateableBehaviour where T : UpdateableBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}
