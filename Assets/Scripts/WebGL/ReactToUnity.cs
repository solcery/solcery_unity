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

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1");
                var boardData = new BoardData();

                boardData.CardTypes = new List<CardType>()
                {
                    new CardType() { CardTypeId = 0, Metadata = new CardMetadata() { Picture = 0, Name = "0", Coins = 0} },
                    new CardType() { CardTypeId = 1, Metadata = new CardMetadata() { Picture = 1, Name = "1", Coins = 1} },
                    new CardType() { CardTypeId = 2, Metadata = new CardMetadata() { Picture = 2, Name = "2", Coins = 2} },
                    new CardType() { CardTypeId = 3, Metadata = new CardMetadata() { Picture = 3, Name = "3", Coins = 3} },
                    new CardType() { CardTypeId = 4, Metadata = new CardMetadata() { Picture = 4, Name = "4", Coins = 4} },
                    new CardType() { CardTypeId = 5, Metadata = new CardMetadata() { Picture = 5, Name = "5", Coins = 5} },
                    new CardType() { CardTypeId = 6, Metadata = new CardMetadata() { Picture = 6, Name = "6", Coins = 6} },
                    new CardType() { CardTypeId = 7, Metadata = new CardMetadata() { Picture = 7, Name = "7", Coins = 7} },
                };

                boardData.Cards = new List<CardData>() {
                    new CardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardTypeId = 0},
                    new CardData() { CardId = 1, CardPlace = CardPlace.Hand1, CardTypeId = 1},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 2},
                    new CardData() { CardId = 3, CardPlace = CardPlace.DrawPile1, CardTypeId = 3},
                    new CardData() { CardId = 4, CardPlace = CardPlace.DrawPile2, CardTypeId = 4},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, CardTypeId = 5},
                    new CardData() { CardId = 6, CardPlace = CardPlace.DrawPile2, CardTypeId = 6},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 3},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 3},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 4},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 4},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 5},
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

                boardData.CardTypes = new List<CardType>()
                {
                    new CardType() { CardTypeId = 0, Metadata = new CardMetadata() { Picture = 10, Name = "10", Coins = 0} },
                    new CardType() { CardTypeId = 1, Metadata = new CardMetadata() { Picture = 11, Name = "11", Coins = 1} },
                    new CardType() { CardTypeId = 2, Metadata = new CardMetadata() { Picture = 12, Name = "12", Coins = 2} },
                    new CardType() { CardTypeId = 3, Metadata = new CardMetadata() { Picture = 13, Name = "13", Coins = 3} },
                    new CardType() { CardTypeId = 4, Metadata = new CardMetadata() { Picture = 14, Name = "14", Coins = 4} },
                };

                boardData.Cards = new List<CardData>() {
                    new CardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardTypeId = 0},
                    new CardData() { CardId = 1, CardPlace = CardPlace.Hand1, CardTypeId = 1},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 2},
                    new CardData() { CardId = 3, CardPlace = CardPlace.DrawPile1, CardTypeId = 3},
                    new CardData() { CardId = 4, CardPlace = CardPlace.DrawPile2, CardTypeId = 4},
                    new CardData() { CardId = 5, CardPlace = CardPlace.Shop, CardTypeId = 4},
                    new CardData() { CardId = 6, CardPlace = CardPlace.DrawPile2, CardTypeId = 3},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 3},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 3},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 4},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 4},
                    new CardData() { CardId = 2, CardPlace = CardPlace.Hand2, CardTypeId = 5},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = true, HP = 6, Coins = 88 },
                    new PlayerData() { IsMe = true, IsActive = false, HP = 19, Coins = 4 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }
        }
#endif
    }
}
