using Grimmz.Modules.CardCollection;
using Grimmz.Modules.FightModule;
using Grimmz.Modules.Wallet;
using Grimmz.Utils;
using UnityEngine;

namespace Grimmz.WebGL
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
            var fight = JsonUtility.FromJson<Fight>(fightJson);
            FightModule.Instance?.UpdateFight(fight);
        }
    }
}
