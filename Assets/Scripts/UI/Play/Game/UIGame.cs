using Solcery.UI.Play.Game.Board;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Play.Game
{
    public class UIGame : Singleton<UIGame>
    {
        [SerializeField] private UIBoard board = null;

        public void Init()
        {
            board?.Init();
        }

        public void DeInit()
        {
            board?.DeInit();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            board?.OnBoardUpdate(boardData);
        }
    }
}
