using Solcery.Modules;
using Solcery.UI.Play;
using Solcery.Utils;

namespace Solcery
{
    public class Play : Singleton<Play>
    {
        public void Init()
        {
            BoardDataDiffTracker.Instance?.Init();
            UIPlay.Instance?.Init();
        }

        public void DeInit()
        {
            BoardDataDiffTracker.Instance?.DeInit();
            UIPlay.Instance?.DeInit();
        }
    }
}
