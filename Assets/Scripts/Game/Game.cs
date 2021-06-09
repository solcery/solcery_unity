using Cysharp.Threading.Tasks;
using Solcery.FSM.Game;
using Solcery.Utils;
using Solcery.Modules.Wallet;
using Solcery.Modules.CardCollection;
using Solcery.WebGL;
using Solcery.Modules.FightModule;
using UnityEngine;

namespace Solcery
{
    public class Game : Singleton<Game>
    {
        public void Init()
        {
            // UnityEngine.Debug.Log("Game Init");

            Wallet.Instance?.Init();
            CardCollection.Instance?.Init();
            FightModule.Instance?.Init();
            UnityToReact.Instance?.CallOnUnityLoaded();
            GameSM.Instance?.PerformInitialTransition().Forget();
        }

        public void DeInit()
        {
            // UnityEngine.Debug.Log("Game DeInit");

            Wallet.Instance?.DeInit();
            CardCollection.Instance?.DeInit();
            FightModule.Instance?.DeInit();
        }
    }
}
