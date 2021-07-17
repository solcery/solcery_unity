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

        [SerializeField] private BoardDataTracker tracker = null;
        [SerializeField] private bool initWithTestJson = false;
        [ShowIf("initWithTestJson")] [Multiline(20)] [SerializeField] private string testJson;

        public void UpdateBoard(BoardData boardData)
        {
            _boardData.Value = boardData;
        }

        public void Init()
        {
            tracker?.Init();

            if (initWithTestJson)
            {
                var boardData = JsonConvert.DeserializeObject<BoardData>(testJson);
                UpdateBoard(boardData.Prettify());
            }
        }

        public void DeInit()
        {
            tracker?.DeInit();

            _boardData = null;
        }
    }
}
