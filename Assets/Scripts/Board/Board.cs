using System;
using Solcery.Utils;

namespace Solcery.Modules.Board
{
    public class Board : Singleton<Board>
    {
        public BoardData BoardData { get; private set; }
        [NonSerialized] public Action<BoardData> OnBoardUpdate;

        public void UpdateBoard(BoardData boardData)
        {
            BoardData = boardData;
            OnBoardUpdate?.Invoke(boardData);
        }

        public void Init()
        {

        }

        public void DeInit()
        {
            
        }
    }
}
