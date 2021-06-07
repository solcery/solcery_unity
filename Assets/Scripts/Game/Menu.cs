using Grimmz.UI.Menu;
using Grimmz.Utils;

namespace Grimmz
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
