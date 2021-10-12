using Newtonsoft.Json;
using Solcery.Modules;
using Solcery.UI;
using Solcery.Utils;

namespace Solcery.React
{
    public class ReactToUnity : Singleton<ReactToUnity>
    {
        public void SetWalletConnected(string data)
        {
            var connectionData = JsonConvert.DeserializeObject<WalletConnectionData>(data);
            Wallet.Instance.Connection.IsConnected.Value = connectionData.IsConnected;
        }

        public void OpenGameOverPopup(string gameOverPopupDataJson)
        {
            var gameOverPopupData = JsonConvert.DeserializeObject<GameOverPopupData>(gameOverPopupDataJson);
            UIGameOverPopup.Instance?.Open(gameOverPopupData);
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
