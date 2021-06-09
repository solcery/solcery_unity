using Solcery.UI.Sandbox;
using Solcery.Utils;

namespace Solcery
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
