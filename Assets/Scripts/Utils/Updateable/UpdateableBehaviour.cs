using Sirenix.OdinInspector;

namespace Grimmz.Utils
{
    public abstract class UpdateableBehaviour : SerializedMonoBehaviour, IUpdateable
    {
        protected virtual void OnEnable()
        {
            Updater.Instance?.Register(this);
        }

        protected virtual void OnDisable()
        {
            Updater.Instance?.Unregister(this);
        }

        public abstract void PerformUpdate();
    }
}
