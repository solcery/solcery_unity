using System.Collections.Generic;

namespace Grimmz.Utils
{
    public class Updater : Singleton<Updater>
    {
        public List<IUpdateable> _updateables = new List<IUpdateable>();

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
            foreach(var u in _updateables)
            {
                u?.PerformUpdate();
            }
        }
    }
}
