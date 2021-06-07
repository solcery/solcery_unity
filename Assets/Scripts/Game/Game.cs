using Cysharp.Threading.Tasks;
using Grimmz.FSM.Game;
using Grimmz.Utils;
using Grimmz.Modules.Wallet;
using Grimmz.Modules.CardCollection;
using Grimmz.WebGL;
using Grimmz.Modules.FightModule;
using UnityEngine;

namespace Grimmz
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
