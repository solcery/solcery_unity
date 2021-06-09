using Solcery.Modules.CardCollection;
using Solcery.Modules.Fight;
using Solcery.Modules.Wallet;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.WebGL
{
    public class ReactToUnity : Singleton<ReactToUnity>
    {
        public void SetWalletConnected(string data)
        {
            var connectionData = JsonUtility.FromJson<WalletConnectionData>(data);
            Wallet.Instance.Connection.IsConnected.Value = connectionData.IsConnected;
        }

        public void UpdateCollection(string collectionJson)
        {
            var collection = JsonUtility.FromJson<Collection>(collectionJson);
            CardCollection.Instance?.UpdateCollection(collection);
        }

        public void UpdateFight(string fightJson)
        {
            var fightData = JsonUtility.FromJson<FightData>(fightJson);
            Fight.Instance?.UpdateFight(fightData);
        }
    }
}
