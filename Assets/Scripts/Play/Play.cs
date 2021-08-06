using Solcery.UI.Play;
using Solcery.Utils;

namespace Solcery
{
    public class Play : Singleton<Play>
    {
        public void Init()
        {
            UIPlay.Instance?.Init();
            BoardDataDiffTracker.Instance?.Init();
            GameResultTracker.Instance?.Init();
        }

        public void DeInit()
        {
            BoardDataDiffTracker.Instance?.DeInit();
            GameResultTracker.Instance?.DeInit();
            UIPlay.Instance?.DeInit();
        }
    }
}
