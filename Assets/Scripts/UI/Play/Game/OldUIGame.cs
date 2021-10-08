using Solcery.UI.Play.Game.Board;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play.Game
{
    public class OldUIGame : Singleton<OldUIGame>
    {
        [SerializeField] private Button exitGameButton = null;
        [SerializeField] private UIBoardNew board = null;

        public void Init()
        {
            exitGameButton?.onClick?.AddListener(() => { GameResultTracker.Instance?.ExitGamePopup(); });
            board?.Init();
        }

        public void DeInit()
        {
            exitGameButton?.onClick?.RemoveAllListeners();
            board?.DeInit();
        }

        public void OnGameContentUpdate(OldGameContent gameContent)
        {
            board?.OnGameContentUpdate(gameContent);
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            board?.OnBoardUpdate(boardData);
        }
    }
}
