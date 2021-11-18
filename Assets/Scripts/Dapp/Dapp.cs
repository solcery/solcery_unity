using Solcery.Utils;
using Solcery.Modules;

namespace Solcery
{
    public class Dapp : Singleton<Dapp>
    {
        public void Init()
        {
            Wallet.Instance?.Init();
            Collection.Instance?.Init();
            LogApplyer.Instance?.Init();
            LogActionCreator.Instance?.Init();
            Board.Instance?.Init();
            Log.Instance?.Init();
            // OldUnityToReact.Instance?.CallOnUnityLoaded();
        }

        public void DeInit()
        {
            Wallet.Instance?.DeInit();
            Collection.Instance?.DeInit();
            LogApplyer.Instance?.DeInit();
            LogActionCreator.Instance?.DeInit();
            Board.Instance?.DeInit();
            Log.Instance?.DeInit();
        }
    }
}
