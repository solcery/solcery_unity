using Solcery.UI.Menu;
using Solcery.Utils;

namespace Solcery
{
    public class Menu : Singleton<Menu>
    {
        public void Init()
        {
            UIMenu.Instance?.Init();
        }

        public void DeInit()
        {
            UIMenu.Instance?.DeInit();
        }
    }
}
