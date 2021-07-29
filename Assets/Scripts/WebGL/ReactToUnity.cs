using System;
using System.Collections.Generic;
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
            Board.Instance?.UpdateBoard(boardData.Prettify());
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

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Test BoardData 1: (first round)");

                var boardData = new BoardData();

                boardData.CardTypes = new List<BoardCardType>()
                {
                    new BoardCardType() { Id = 0, Metadata = new CardMetadata() { Picture = 0, Name = "Name 0", Coins = 0, Description = "Description 0"} },
                    new BoardCardType() { Id = 1, Metadata = new CardMetadata() { Picture = 1, Name = "Name 1", Coins = 1, Description = "Description 1"} },
                    new BoardCardType() { Id = 2, Metadata = new CardMetadata() { Picture = 2, Name = "Name 2", Coins = 2, Description = "Description 2"} },
                    new BoardCardType() { Id = 3, Metadata = new CardMetadata() { Picture = 3, Name = "Name 3", Coins = 3, Description = "Description 3"} },
                    new BoardCardType() { Id = 4, Metadata = new CardMetadata() { Picture = 4, Name = "Name 4", Coins = 4, Description = "Description 4"} },
                    new BoardCardType() { Id = 5, Metadata = new CardMetadata() { Picture = 5, Name = "Name 5", Coins = 5, Description = "Description 5"} },
                    new BoardCardType() { Id = 6, Metadata = new CardMetadata() { Picture = 6, Name = "Name 6", Coins = 6, Description = "Description 6"} },
                    new BoardCardType() { Id = 7, Metadata = new CardMetadata() { Picture = 7, Name = "Name 7", Coins = 7, Description = "Description 7"} },
                };

                boardData.Cards = new List<BoardCardData>() {
                    new BoardCardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardType = 0},

                    new BoardCardData() { CardId = 1, CardPlace = CardPlace.Deck, CardType = 1},

                    new BoardCardData() { CardId = 2, CardPlace = CardPlace.Shop, CardType = 1},

                    new BoardCardData() { CardId = 3, CardPlace = CardPlace.Hand1, CardType = 1},
                    new BoardCardData() { CardId = 4, CardPlace = CardPlace.Hand1, CardType = 2},
                    new BoardCardData() { CardId = 5, CardPlace = CardPlace.Hand1, CardType = 3},

                    new BoardCardData() { CardId = 6, CardPlace = CardPlace.Hand2, CardType = 4},
                    new BoardCardData() { CardId = 7, CardPlace = CardPlace.Hand2, CardType = 5},
                    new BoardCardData() { CardId = 8, CardPlace = CardPlace.Hand2, CardType = 6},

                    new BoardCardData() { CardId = 9, CardPlace = CardPlace.DrawPile1, CardType = 1},

                    new BoardCardData() { CardId = 10, CardPlace = CardPlace.DrawPile2, CardType = 1},

                    new BoardCardData() { CardId = 11, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},
                    new BoardCardData() { CardId = 12, CardPlace = CardPlace.PlayedThisTurn, CardType = 2},

                    new BoardCardData() { CardId = 13, CardPlace = CardPlace.PlayedThisTurnTop, CardType = 1},

                    new BoardCardData() { CardId = 14, CardPlace = CardPlace.DiscardPile1, CardType = 1},

                    new BoardCardData() { CardId = 15, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = true, HP = 20, Coins = 0 },
                    new PlayerData() { IsMe = true, IsActive = false, HP = 20, Coins = 0 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());

                // UpdateBoard(testJson);
                // Log.Instance?.Init();
                // Log.Instance?.CastCard(1, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // Log.Instance?.CastCard(2, 1);
                Debug.Log("Test BoardData 2: (played a card, dealt some damage, gained some coins");

                var boardData = new BoardData();

                boardData.CardTypes = new List<BoardCardType>()
                {
                    new BoardCardType() { Id = 0, Metadata = new CardMetadata() { Picture = 0, Name = "Name 0", Coins = 0, Description = "Description 0"} },
                    new BoardCardType() { Id = 1, Metadata = new CardMetadata() { Picture = 1, Name = "Name 1", Coins = 1, Description = "Description 1"} },
                    new BoardCardType() { Id = 2, Metadata = new CardMetadata() { Picture = 2, Name = "Name 2", Coins = 2, Description = "Description 2"} },
                    new BoardCardType() { Id = 3, Metadata = new CardMetadata() { Picture = 3, Name = "Name 3", Coins = 3, Description = "Description 3"} },
                    new BoardCardType() { Id = 4, Metadata = new CardMetadata() { Picture = 4, Name = "Name 4", Coins = 4, Description = "Description 4"} },
                    new BoardCardType() { Id = 5, Metadata = new CardMetadata() { Picture = 5, Name = "Name 5", Coins = 5, Description = "Description 5"} },
                    new BoardCardType() { Id = 6, Metadata = new CardMetadata() { Picture = 6, Name = "Name 6", Coins = 6, Description = "Description 6"} },
                    new BoardCardType() { Id = 7, Metadata = new CardMetadata() { Picture = 7, Name = "Name 7", Coins = 7, Description = "Description 7"} },
                };

                boardData.Cards = new List<BoardCardData>() {
                    new BoardCardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardType = 0},

                    new BoardCardData() { CardId = 1, CardPlace = CardPlace.Deck, CardType = 1},

                    new BoardCardData() { CardId = 2, CardPlace = CardPlace.Shop, CardType = 1},

                    new BoardCardData() { CardId = 3, CardPlace = CardPlace.Hand1, CardType = 1},
                    new BoardCardData() { CardId = 4, CardPlace = CardPlace.Hand1, CardType = 2},
                    new BoardCardData() { CardId = 5, CardPlace = CardPlace.Hand1, CardType = 3},

                    new BoardCardData() { CardId = 6, CardPlace = CardPlace.Hand2, CardType = 4},
                    new BoardCardData() { CardId = 8, CardPlace = CardPlace.Hand2, CardType = 6},

                    new BoardCardData() { CardId = 9, CardPlace = CardPlace.DrawPile1, CardType = 1},

                    new BoardCardData() { CardId = 10, CardPlace = CardPlace.DrawPile2, CardType = 1},

                    new BoardCardData() { CardId = 11, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},
                    new BoardCardData() { CardId = 12, CardPlace = CardPlace.PlayedThisTurn, CardType = 2},
                    new BoardCardData() { CardId = 13, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},

                    new BoardCardData() { CardId = 7, CardPlace = CardPlace.PlayedThisTurnTop, CardType = 5},

                    new BoardCardData() { CardId = 14, CardPlace = CardPlace.DiscardPile1, CardType = 1},

                    new BoardCardData() { CardId = 15, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = false, HP = 17, Coins = 0 },
                    new PlayerData() { IsMe = true, IsActive = true, HP = 20, Coins = 2 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Test BoardData 3: (bought a card from shop)");

                var boardData = new BoardData();

                boardData.CardTypes = new List<BoardCardType>()
                {
                    new BoardCardType() { Id = 0, Metadata = new CardMetadata() { Picture = 0, Name = "Name 0", Coins = 0, Description = "Description 0"} },
                    new BoardCardType() { Id = 1, Metadata = new CardMetadata() { Picture = 1, Name = "Name 1", Coins = 1, Description = "Description 1"} },
                    new BoardCardType() { Id = 2, Metadata = new CardMetadata() { Picture = 2, Name = "Name 2", Coins = 2, Description = "Description 2"} },
                    new BoardCardType() { Id = 3, Metadata = new CardMetadata() { Picture = 3, Name = "Name 3", Coins = 3, Description = "Description 3"} },
                    new BoardCardType() { Id = 4, Metadata = new CardMetadata() { Picture = 4, Name = "Name 4", Coins = 4, Description = "Description 4"} },
                    new BoardCardType() { Id = 5, Metadata = new CardMetadata() { Picture = 5, Name = "Name 5", Coins = 5, Description = "Description 5"} },
                    new BoardCardType() { Id = 6, Metadata = new CardMetadata() { Picture = 6, Name = "Name 6", Coins = 6, Description = "Description 6"} },
                    new BoardCardType() { Id = 7, Metadata = new CardMetadata() { Picture = 7, Name = "Name 7", Coins = 7, Description = "Description 7"} },
                };

                boardData.Cards = new List<BoardCardData>() {
                    new BoardCardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardType = 0},

                    new BoardCardData() { CardId = 1, CardPlace = CardPlace.Deck, CardType = 1},

                    // shop

                    new BoardCardData() { CardId = 3, CardPlace = CardPlace.Hand1, CardType = 1},
                    new BoardCardData() { CardId = 4, CardPlace = CardPlace.Hand1, CardType = 2},
                    new BoardCardData() { CardId = 5, CardPlace = CardPlace.Hand1, CardType = 3},

                    new BoardCardData() { CardId = 6, CardPlace = CardPlace.Hand2, CardType = 4},
                    new BoardCardData() { CardId = 8, CardPlace = CardPlace.Hand2, CardType = 6},

                    new BoardCardData() { CardId = 9, CardPlace = CardPlace.DrawPile1, CardType = 1},

                    new BoardCardData() { CardId = 10, CardPlace = CardPlace.DrawPile2, CardType = 1},

                    new BoardCardData() { CardId = 11, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},
                    new BoardCardData() { CardId = 12, CardPlace = CardPlace.PlayedThisTurn, CardType = 2},
                    new BoardCardData() { CardId = 13, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},

                    new BoardCardData() { CardId = 7, CardPlace = CardPlace.PlayedThisTurnTop, CardType = 5},

                    new BoardCardData() { CardId = 14, CardPlace = CardPlace.DiscardPile1, CardType = 1},

                    new BoardCardData() { CardId = 15, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                    new BoardCardData() { CardId = 2, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = false, HP = 17, Coins = 0 },
                    new PlayerData() { IsMe = true, IsActive = true, HP = 20, Coins = 1 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Test BoardData 4: (ended turn)");

                var boardData = new BoardData();

                boardData.CardTypes = new List<BoardCardType>()
                {
                    new BoardCardType() { Id = 0, Metadata = new CardMetadata() { Picture = 0, Name = "Name 0", Coins = 0, Description = "Description 0"} },
                    new BoardCardType() { Id = 1, Metadata = new CardMetadata() { Picture = 1, Name = "Name 1", Coins = 1, Description = "Description 1"} },
                    new BoardCardType() { Id = 2, Metadata = new CardMetadata() { Picture = 2, Name = "Name 2", Coins = 2, Description = "Description 2"} },
                    new BoardCardType() { Id = 3, Metadata = new CardMetadata() { Picture = 3, Name = "Name 3", Coins = 3, Description = "Description 3"} },
                    new BoardCardType() { Id = 4, Metadata = new CardMetadata() { Picture = 4, Name = "Name 4", Coins = 4, Description = "Description 4"} },
                    new BoardCardType() { Id = 5, Metadata = new CardMetadata() { Picture = 5, Name = "Name 5", Coins = 5, Description = "Description 5"} },
                    new BoardCardType() { Id = 6, Metadata = new CardMetadata() { Picture = 6, Name = "Name 6", Coins = 6, Description = "Description 6"} },
                    new BoardCardType() { Id = 7, Metadata = new CardMetadata() { Picture = 7, Name = "Name 7", Coins = 7, Description = "Description 7"} },
                };

                boardData.Cards = new List<BoardCardData>() {
                    new BoardCardData() { CardId = 0, CardPlace = CardPlace.Nowhere, CardType = 0},

                    new BoardCardData() { CardId = 1, CardPlace = CardPlace.Deck, CardType = 1},

                    // shop

                    new BoardCardData() { CardId = 3, CardPlace = CardPlace.Hand1, CardType = 1},
                    new BoardCardData() { CardId = 4, CardPlace = CardPlace.Hand1, CardType = 2},
                    new BoardCardData() { CardId = 5, CardPlace = CardPlace.Hand1, CardType = 3},

                    new BoardCardData() { CardId = 6, CardPlace = CardPlace.Hand2, CardType = 4},
                    new BoardCardData() { CardId = 8, CardPlace = CardPlace.Hand2, CardType = 6},

                    new BoardCardData() { CardId = 9, CardPlace = CardPlace.DrawPile1, CardType = 1},

                    new BoardCardData() { CardId = 10, CardPlace = CardPlace.DrawPile2, CardType = 1},

                    new BoardCardData() { CardId = 11, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},
                    new BoardCardData() { CardId = 12, CardPlace = CardPlace.PlayedThisTurn, CardType = 2},
                    new BoardCardData() { CardId = 13, CardPlace = CardPlace.PlayedThisTurn, CardType = 1},

                    new BoardCardData() { CardId = 7, CardPlace = CardPlace.PlayedThisTurnTop, CardType = 5},

                    new BoardCardData() { CardId = 14, CardPlace = CardPlace.DiscardPile1, CardType = 1},

                    new BoardCardData() { CardId = 15, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                    new BoardCardData() { CardId = 2, CardPlace = CardPlace.DiscardPile2, CardType = 1},
                };

                boardData.Players = new List<PlayerData>() {
                    new PlayerData() { IsMe = false, IsActive = true, HP = 17, Coins = 0 },
                    new PlayerData() { IsMe = true, IsActive = false, HP = 20, Coins = 1 }
                };

                Board.Instance?.UpdateBoard(boardData.Prettify());
            }


            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("Game Over");
                var gameOverData = new GameOverData()
                {
                    Title = "El victory",
                    Description = "You demolished your opponent. Gratz!",
                    Callback = "callback"
                };

                GameOverPopupWithDelay(gameOverData).Forget();
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Debug.Log("Test CollectionData 1");

                var collectionData = new CollectionData();

                collectionData.CardTypes = new List<CollectionCardType>() {
                    new CollectionCardType() { MintAddress = "1", Metadata = new CardMetadata() { Picture = 1, Coins = 1, Name = "1", Description = "1"}},
                    new CollectionCardType() { MintAddress = "2", Metadata = new CardMetadata() { Picture = 2, Coins = 2, Name = "2", Description = "2"}},
                    new CollectionCardType() { MintAddress = "3", Metadata = new CardMetadata() { Picture = 3, Coins = 3, Name = "3", Description = "3"}},
                };

                Collection.Instance?.UpdateCollection(collectionData);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Debug.Log("Test CollectionData 2");

                var collectionData = new CollectionData();

                collectionData.CardTypes = new List<CollectionCardType>() {
                    new CollectionCardType() { MintAddress = "1", Metadata = new CardMetadata() { Picture = 1, Coins = 1, Name = "1", Description = "1"}},
                    new CollectionCardType() { MintAddress = "2", Metadata = new CardMetadata() { Picture = 2, Coins = 2, Name = "2", Description = "2"}},
                    new CollectionCardType() { MintAddress = "3", Metadata = new CardMetadata() { Picture = 3, Coins = 3, Name = "3", Description = "3"}},
                    new CollectionCardType() { MintAddress = "4", Metadata = new CardMetadata() { Picture = 4, Coins = 4, Name = "4", Description = "4"}},
                    new CollectionCardType() { MintAddress = "5", Metadata = new CardMetadata() { Picture = 5, Coins = 5, Name = "5", Description = "5"}},
                    new CollectionCardType() { MintAddress = "6", Metadata = new CardMetadata() { Picture = 6, Coins = 6, Name = "6", Description = "6"}},
                    new CollectionCardType() { MintAddress = "7", Metadata = new CardMetadata() { Picture = 7, Coins = 7, Name = "7", Description = "7"}},
                    new CollectionCardType() { MintAddress = "8", Metadata = new CardMetadata() { Picture = 8, Coins = 8, Name = "8", Description = "8"}},
                    new CollectionCardType() { MintAddress = "9", Metadata = new CardMetadata() { Picture = 9, Coins = 9, Name = "9", Description = "9"}},
                };

                Collection.Instance?.UpdateCollection(collectionData);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Board.Instance?.UpdateBoard(null);
            }
        }
#endif
    }
}
