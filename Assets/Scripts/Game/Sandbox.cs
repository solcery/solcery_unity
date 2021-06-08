using Grimmz.UI.Sandbox;
using Grimmz.Utils;

namespace Grimmz
{
    public class Sandbox : Singleton<Sandbox>
    {
        public void Init()
        {
            UISandbox.Instance?.Init();
        }

        public void DeInit()
        {
            UISandbox.Instance?.DeInit();
        }
    }
}
