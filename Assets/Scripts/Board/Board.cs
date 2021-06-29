using System;
using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.Board
{
    public class Board : Singleton<Board>
    {
        public BoardData BoardData { get; private set; }
        [NonSerialized] public Action<BoardData> OnBoardUpdate;

        // public AsyncReactiveProperty<BoardData> BoardData => _boardData;
        // private AsyncReactiveProperty<BoardData> _boardData = new AsyncReactiveProperty<BoardData>(null);

        public void UpdateBoard(BoardData boardData)
        {
            BoardData = boardData;
            OnBoardUpdate?.Invoke(boardData);
            // _boardData.Value = boardData;
        }

        public void Init()
        {

        }

        public void DeInit()
        {

        }
    }
}
