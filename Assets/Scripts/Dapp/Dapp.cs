using Solcery.FSM.Dapp;
using Solcery.Utils;
using Solcery.Modules.Wallet;
using Solcery.Modules.Collection;
using Solcery.Modules.Board;
using Solcery.WebGL;
using Solcery.Modules.Log;

namespace Solcery
{
    public class Dapp : Singleton<Dapp>
    {
        public void Init()
        {
            Wallet.Instance?.Init();
            Collection.Instance?.Init();
            Board.Instance?.Init();
            Log.Instance?.Init();
            UnityToReact.Instance?.CallOnUnityLoaded();
            DappSM.Instance?.PerformInitialTransition();
        }

        public void DeInit()
        {
            Wallet.Instance?.DeInit();
            Collection.Instance?.DeInit();
            Board.Instance?.DeInit();
            Log.Instance?.DeInit();
        }
    }
}
