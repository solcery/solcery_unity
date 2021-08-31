using Solcery.Utils;
using Solcery.Modules;
using Solcery.WebGL;

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
            UnityToReact.Instance?.CallOnUnityLoaded();
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
