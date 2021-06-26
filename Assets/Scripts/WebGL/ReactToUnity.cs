using System;
using System.Collections.Generic;
using Solcery.Modules.Board;
using Solcery.Modules.Collection;
using Solcery.Modules.Wallet;
using Solcery.Utils;
using UnityEngine;

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
            var collectionData = JsonUtility.FromJson<CollectionData>(collectionJson);
            Collection.Instance?.UpdateCollection(collectionData);
        }

        public void UpdateBoard(string boardJson)
        {
            var boardData = JsonUtility.FromJson<BoardData>(boardJson);
            var prettifiedBoardData = boardData.Prettify();
            Board.Instance?.UpdateBoard(prettifiedBoardData);
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


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1");
                var boardData = new BoardData();

                boardData.Cards = new List<CardData>() {
                    new CardData() { CardId = 0, CardPlace = CardPlace.Nowhere, Metadata = new CardMetadata() { Picture = 0, Name = "0"}},
                    new CardData() { CardId = 1, CardPlace = CardPlace.Hand1, Metadata = new CardMetadata() { Picture = 1, Name = "1"}},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, Metadata = new CardMetadata() { Picture = 2, Name = "2"}},
                    new CardData() { CardId = 3, CardPlace = CardPlace.DrawPile1, Metadata = new CardMetadata() { Picture = 3, Name = "3"}},
                    new CardData() { CardId = 4, CardPlace = CardPlace.DrawPile2, Metadata = new CardMetadata() { Picture = 4, Name = "4"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, Metadata = new CardMetadata() { Picture = 5, Name = "5"}},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = true, HP = 15, Coins = 36 },
                    new PlayerData() { IsMe = true, IsActive = false, HP = 7, Coins = 12 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2");
                var boardData = new BoardData();

                boardData.Cards = new List<CardData>() {
                    new CardData() { CardId = 0, CardPlace = CardPlace.Nowhere, Metadata = new CardMetadata() { Picture = 0, Name = "0"}},
                    new CardData() { CardId = 1, CardPlace = CardPlace.Hand1, Metadata = new CardMetadata() { Picture = 11, Name = "11"}},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, Metadata = new CardMetadata() { Picture = 12, Name = "12"}},
                    new CardData() { CardId = 3, CardPlace = CardPlace.DrawPile1, Metadata = new CardMetadata() { Picture = 13, Name = "13"}},
                    new CardData() { CardId = 4, CardPlace = CardPlace.DrawPile2, Metadata = new CardMetadata() { Picture = 14, Name = "14"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, Metadata = new CardMetadata() { Picture = 15, Name = "15"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, Metadata = new CardMetadata() { Picture = 16, Name = "16"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, Metadata = new CardMetadata() { Picture = 17, Name = "17"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Deck, Metadata = new CardMetadata() { Picture = 18, Name = "18"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Deck, Metadata = new CardMetadata() { Picture = 19, Name = "19"}},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Deck, Metadata = new CardMetadata() { Picture = 20, Name = "20"}},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = false, HP = 8, Coins = 99 },
                    new PlayerData() { IsMe = true, IsActive = true, HP = 17, Coins = 69 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }
        }
    }
}
