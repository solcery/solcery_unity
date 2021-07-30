using System;
using Solcery.Modules;
using Solcery.Utils;
using UnityEngine;
using Newtonsoft.Json;
using Solcery.UI.Play;
using Cysharp.Threading.Tasks;

namespace Solcery.WebGL
{
    public class ReactToUnity : Singleton<ReactToUnity>
    {
        public static Action<CardCreationSignData> OnCardCreationSignDataChanged;
        public static Action<CardCreationConfirmData> OnCardCreationConfirmDataChanged;

        public void SetWalletConnected(string data)
        {
            var connectionData = JsonUtility.FromJson<WalletConnectionData>(data);
            Wallet.Instance.Connection.IsConnected.Value = connectionData.IsConnected;
        }

        public void UpdateCollection(string collectionJson)
        {
            var collectionData = JsonConvert.DeserializeObject<CollectionData>(collectionJson);
            Collection.Instance?.UpdateCollection(collectionData.Prettify());
        }

        public void UpdateLog(string logJson)
        {
            var logData = JsonConvert.DeserializeObject<LogData>(logJson);
            Log.Instance?.UpdateLog(logData);
        }

        public void UpdateBoard(string boardJson)
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(boardJson);
            Board.Instance?.UpdateBoard(boardData.Prettify(isVirgin: true));
        }

        public void SetCardCreationSigned(string signJson)
        {
            var signData = JsonUtility.FromJson<CardCreationSignData>(signJson);
            OnCardCreationSignDataChanged?.Invoke(signData);
        }

        public void SetCardCreationConfirmed(string confirmJson)
        {
            var confirmData = JsonUtility.FromJson<CardCreationConfirmData>(confirmJson);
            OnCardCreationConfirmDataChanged?.Invoke(confirmData);
        }

        public void SetGameOver(string gameOverJson)
        {
            var gameOverData = JsonConvert.DeserializeObject<GameOverData>(gameOverJson);
            GameOverPopupWithDelay(gameOverData).Forget();
        }

        private async UniTaskVoid GameOverPopupWithDelay(GameOverData gameOverData)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            UIGameOverPopup.Instance?.Open(gameOverData);
        }

        // Debug.Log("Game Over");
        //         var gameOverData = new GameOverData()
        //         {
        //             Title = "El victory",
        //             Description = "You demolished your opponent. Gratz!",
        //             Callback = "callback"
        //         };

        //         GameOverPopupWithDelay(gameOverData).Forget();
    }
}
