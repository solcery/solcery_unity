using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.Board
{
    public class Board : Singleton<Board>
    {
        public AsyncReactiveProperty<BoardData> BoardData => _boardData;
        private AsyncReactiveProperty<BoardData> _boardData = new AsyncReactiveProperty<BoardData>(null);

        public void UpdateBoard(BoardData boardData)
        {
            _boardData.Value = boardData;
        }

        public void Init()
        {
            
        }

        public void DeInit()
        {
            _boardData = null;
        }
    }
}
