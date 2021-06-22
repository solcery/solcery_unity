using System;
using Solcery.Utils;

namespace Solcery
{
    public class Board : Singleton<Board>
    {
        [NonSerialized] public Action<BoardData> OnBoardUpdate;

        public void UpdateBoard(BoardData boardData)
        {
            OnBoardUpdate?.Invoke(boardData);
        }
    }
}
