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
            if (boardData != null && boardData.IsVirgin)
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
            UpdateBoard(boardData.Prettify(isVirgin: true));
        }

        public void UpdateWithJson1()
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(json1);
            UpdateBoard(boardData.Prettify(isVirgin: true));
        }

        public void UpdateWithJson2()
        {
            var boardData = JsonConvert.DeserializeObject<BoardData>(json2);
            UpdateBoard(boardData.Prettify(isVirgin: true));
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
                UpdateBoard(boardData.Prettify(isVirgin: true));
            }
        }
    }
}
