using Cysharp.Threading.Tasks;
using Solcery.FSM.Game;
using Solcery.Utils;
using Solcery.Modules.Wallet;
using Solcery.Modules.CardCollection;
using Solcery.WebGL;
using Solcery.Modules.Fight;

namespace Solcery
{
    public class Game : Singleton<Game>
    {
        public void Init()
        {
            Wallet.Instance?.Init();
            CardCollection.Instance?.Init();
            Fight.Instance?.Init();
            UnityToReact.Instance?.CallOnUnityLoaded();
            GameSM.Instance?.PerformInitialTransition();
        }

        public void DeInit()
        {
            Wallet.Instance?.DeInit();
            CardCollection.Instance?.DeInit();
            Fight.Instance?.DeInit();
        }
    }
}
