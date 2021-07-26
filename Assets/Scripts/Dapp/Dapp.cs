using Solcery.FSM.Game;
using Solcery.Utils;
using Solcery.Modules.Wallet;
using Solcery.Modules.Collection;
using Solcery.Modules.Board;
using Solcery.WebGL;

namespace Solcery
{
    public class Dapp : Singleton<Dapp>
    {
        public void Init()
        {
            Wallet.Instance?.Init();
            Collection.Instance?.Init();
            Board.Instance?.Init();
            UnityToReact.Instance?.CallOnUnityLoaded();
            DappSM.Instance?.PerformInitialTransition();
        }

        public void DeInit()
        {
            Wallet.Instance?.DeInit();
            Collection.Instance?.DeInit();
            Board.Instance?.DeInit();
        }
    }
}
