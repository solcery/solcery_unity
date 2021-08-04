using System.Collections.Generic;

namespace Solcery.Utils
{
    public class Updater : Singleton<Updater>
    {
        public List<IUpdateable> _updateables = new List<IUpdateable>();
        private List<IUpdateable> _updateablesLock;

        public void Register(IUpdateable updateable)
        {
            _updateables.Add(updateable);
        }

        public void Unregister(IUpdateable updateable)
        {
            _updateables.Remove(updateable);
        }

        void Update()
        {
            _updateablesLock = new List<IUpdateable>(_updateables);

            foreach (var u in _updateablesLock)
            {
                if (u != null)
                    u?.PerformUpdate();
            }
        }
    }
}
