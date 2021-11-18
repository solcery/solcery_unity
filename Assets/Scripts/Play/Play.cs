using Solcery.Modules;
using Solcery.Utils;
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
            Log.Instance?.Init();

            // OldUnityToReact.Instance?.CallOnUnityLoaded();

            // PlaySM.Instance?.Enter();
        }

        public void OnDisable()
        {
            // PlaySM.Instance?.Exit();

            Wallet.Instance?.DeInit();
            Collection.Instance?.DeInit();
            LogApplyer.Instance?.DeInit();
            LogActionCreator.Instance?.DeInit();
            Board.Instance?.DeInit();
            Log.Instance?.DeInit();
        }
    }
}
