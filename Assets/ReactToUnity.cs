using Solcery.Modules;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.React
{
    public class ReactToUnity : Singleton<ReactToUnity>
    {
        public void SetWalletConnected(string data)
        {
            var connectionData = JsonUtility.FromJson<WalletConnectionData>(data);
            Wallet.Instance.Connection.IsConnected.Value = connectionData.IsConnected;
        }

        public void UpdateGameContent(string gameContentJson)
        {
            Game.Instance?.UpdateGameContent(gameContentJson);
        }

        public void UpdateGameDisplay(string gameDisplayJson)
        {
            Game.Instance?.UpdateGameDisplay(gameDisplayJson);
        }

        public void UpdateGameState(string gameStateJson)
        {
            Game.Instance?.UpdateGameState(gameStateJson);
        }
    }
}
