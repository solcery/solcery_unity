using Solcery.Modules.Collection;
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
            var collectionData = JsonUtility.FromJson<CollectionData>(collectionJson);
            Collection.Instance?.UpdateCollection(collectionData);
        }

        public void UpdateFight(string fightJson)
        {
            var fightData = JsonUtility.FromJson<FightData>(fightJson);
            Fight.Instance?.UpdateFight(fightData);
        }

        public void SetCardCreationSigned(string signJson)
        {
            var signData = JsonUtility.FromJson<CardCreationSignData>(signJson);
            Debug.Log($"signed: {signData.IsSigned}");
        }

        public void SetCardCreationConfirmed(string confirmJson)
        {
            var confirmData = JsonUtility.FromJson<CardCreationConfirmData>(confirmJson);
            Debug.Log($"confirmed: {confirmData.IsConfirmed}");
        }
    }
}
