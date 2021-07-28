using Solcery.Modules.Board;
using Solcery.UI.Play;
using Solcery.Utils;

namespace Solcery
{
    public class Play : Singleton<Play>
    {
        public void Init()
        {
            BoardDataTracker.Instance?.Init();
            UIPlay.Instance?.Init();
        }

        public void DeInit()
        {
            BoardDataTracker.Instance?.DeInit();
            UIPlay.Instance?.DeInit();
        }
    }
}
