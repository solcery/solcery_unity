using Sirenix.OdinInspector;

namespace Solcery.Utils
{
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        public static T Instance { get; private set; }
        
        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}
