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

        public void SaveBoardData1()
        {
            Debug.Log("Saving");

            var boardData = JsonConvert.DeserializeObject<BoardData>(json1);
            boardData.DisplayData = new BoardDisplayData();
            boardData.DisplayData.PlaceDisplayDatas.Add(new PlaceDisplayData()
            {
                PlaceName = "PlayerHand",
                PlaceId = 3,
                IsVisible = true,
                HorizontalAnchors = new PlaceDisplayAnchors(0.3f, 0.7f),
                VerticalAnchors = new PlaceDisplayAnchors(0.1f, 0.4f),
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
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
                VerticalAnchors = new PlaceDisplayAnchors(0.025f, 0.35f),
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
                CardFaceOption = CardFaceOption.Up,
                CardLayoutOption = CardLayoutOption.LayedOut
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
