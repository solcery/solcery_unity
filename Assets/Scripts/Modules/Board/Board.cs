using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.Modules
{
    public class Board : Singleton<Board>
    {
        public AsyncReactiveProperty<BoardData> BoardData => _boardData;
        private AsyncReactiveProperty<BoardData> _boardData = new AsyncReactiveProperty<BoardData>(null);

        [SerializeField] private bool initWithTestJson = false;
        [ShowIf("initWithTestJson")] [Multiline(20)] [SerializeField] private string testJson;
        [Multiline(20)] [SerializeField] private string json1;
        [Multiline(20)] [SerializeField] private string json2;

        public void UpdateBoard(BoardData boardData)
        {
            if (boardData != null)
            {
                _boardData.Value = LogApplyer.Instance.ApplyCurrentLog(boardData);
            }
            else
            {
                _boardData.Value = boardData;
            }
        }

        public void UpdateWithTestJson()
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(testJson);
            UpdateBoard(boardData.Prettify());
        }

        public void UpdateWithJson1()
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(json1);
            UpdateBoard(boardData.Prettify());
        }

        public void UpdateWithJson2()
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(json2);
            UpdateBoard(boardData.Prettify());
        }

        public void SaveGameContent1()
        {
            Debug.Log("Saving GameContent");

            var boardData = JsonConvert.DeserializeObject<BoardData>(json1);
            var gameContent = new GameContent();
            gameContent.DisplayData = boardData.DisplayData;
            gameContent.CardTypes = boardData.CardTypes;

            var filePath = Application.streamingAssetsPath + "/" + "GameContent" + ".json";
            string json = JsonConvert.SerializeObject(gameContent, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public void SaveBoardData1()
        {
            Debug.Log("Saving");

            var boardData = JsonConvert.DeserializeObject<BoardData>(json1);
            boardData.DisplayData = new BoardDisplayData();
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "Deck",
                PlaceId = 1,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "Shop",
                PlaceId = 2,
                Player = PlacePlayer.Common,
                IsVisible = true,
                AreCardsInteractableIfMeIsActive = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerHand",
                PlaceId = 3,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyHand",
                PlaceId = 4,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerDrawPile",
                PlaceId = 5,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyDrawPile",
                PlaceId = 6,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayedThisTurn",
                PlaceId = 7,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayedThisTurnTop",
                PlaceId = 8,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerDiscardPile",
                PlaceId = 9,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyDiscardPile",
                PlaceId = 10,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            var filePath = Application.streamingAssetsPath + "/" + "BoardDataWithDisplay1" + ".json";
            string json = JsonConvert.SerializeObject(boardData, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public void SaveBoardData2()
        {
            Debug.Log("Saving");

            var boardData = JsonConvert.DeserializeObject<BoardData>(json2);
            boardData.DisplayData = new BoardDisplayData();
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "Deck",
                PlaceId = 1,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "Shop",
                PlaceId = 2,
                Player = PlacePlayer.Common,
                IsVisible = true,
                AreCardsInteractableIfMeIsActive = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerHand",
                PlaceId = 3,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyHand",
                PlaceId = 4,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.0f, 0.6f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.LayedOut
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerDrawPile",
                PlaceId = 5,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyDrawPile",
                PlaceId = 6,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.75f, 0.85f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayedThisTurn",
                PlaceId = 7,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayedThisTurnTop",
                PlaceId = 8,
                Player = PlacePlayer.Common,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.375f, 0.7f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerDiscardPile",
                PlaceId = 9,
                Player = PlacePlayer.Player,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.03f, 0.35f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "EnemyDiscardPile",
                PlaceId = 10,
                Player = PlacePlayer.Enemy,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.64f, 0.74f),
                VerticalAnchors = new PlaceDisplayAnchors(0.75f, 1.0f),
                CardFaceOption = CardFaceOption.Down,
                CardLayoutOption = CardLayoutOption.Stacked
            });
            var filePath = Application.streamingAssetsPath + "/" + "BoardDataWithDisplay2" + ".json";
            string json = JsonConvert.SerializeObject(boardData, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public void Init()
        {
            InitWithJson();
        }

        public void DeInit()
        {
            _boardData = null;
        }

        private void InitWithJson()
        {
            if (initWithTestJson)
            {
                var boardData = JsonConvert.DeserializeObject<BoardData>(testJson);
                UpdateBoard(boardData.Prettify());
            }
        }
    }
}
