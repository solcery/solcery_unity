using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.Modules.Board
{
    public class Board : Singleton<Board>
    {
        public AsyncReactiveProperty<BoardData> BoardData => _boardData;
        private AsyncReactiveProperty<BoardData> _boardData = new AsyncReactiveProperty<BoardData>(null);

        [SerializeField] private bool initWithTestJson = false;
        [ShowIf("initWithTestJson")] [Multiline(20)] [SerializeField] private string testJson;

        public void UpdateBoard(BoardData boardData)
        {
            Debug.Log("Board.UpdateBoard");

            _boardData.Value = boardData;
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
