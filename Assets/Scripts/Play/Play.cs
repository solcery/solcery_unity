using Solcery.UI.Play;
using Solcery.Utils;

namespace Solcery
{
    public class Play : Singleton<Play>
    {
        public void Init()
        {
            UIPlay.Instance?.Init();
        }

        public void DeInit()
        {
            UIPlay.Instance?.DeInit();
        }
    }
}
