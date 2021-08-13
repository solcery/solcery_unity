using Solcery.FSM.Play;
using Solcery.Modules;
using Solcery.Utils;
using Solcery.WebGL;

namespace Solcery
{
    public class Play : Singleton<Play>
    {
        public void Start()
        {
            Wallet.Instance?.Init();
            Collection.Instance?.Init();
            LogApplyer.Instance?.Init();
            LogActionCreator.Instance?.Init();
            Board.Instance?.Init();
            BoardDataDiffTracker.Instance?.Init();
            Log.Instance?.Init();
            PlayerGameStatusTracker.Instance?.Init();

            UnityToReact.Instance?.CallOnUnityLoaded();

            PlaySM.Instance?.Enter();
        }

        public void OnDisable()
        {
            PlaySM.Instance?.Exit();

            Wallet.Instance?.DeInit();
            Collection.Instance?.DeInit();
            LogApplyer.Instance?.DeInit();
            LogActionCreator.Instance?.DeInit();
            Board.Instance?.DeInit();
            BoardDataDiffTracker.Instance?.DeInit();
            Log.Instance?.DeInit();
            PlayerGameStatusTracker.Instance?.DeInit();
        }
    }
}
