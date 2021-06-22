using Solcery.Modules.Board;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Solcery.UI.Play
{
    public class UIPlay : Singleton<UIPlay>
    {
        [SerializeField] private Button createGameButton = null;
        [SerializeField] private Button joinGameButton = null;
        [SerializeField] private TextMeshProUGUI joinGameKey = null;
        [SerializeField] private UIBoard board = null;

        public void Init()
        {
            if (Board.Instance.BoardData == null)
            {
                createGameButton.onClick.AddListener(() =>
                {
                    UnityToReact.Instance?.CallCreateBoard();
                    createGameButton.gameObject.SetActive(false);
                    joinGameButton.gameObject.SetActive(false);
                });

                joinGameButton.onClick.AddListener(() =>
                {
                    if (joinGameKey != null && !string.IsNullOrEmpty(joinGameKey.text))
                    {
                        UnityToReact.Instance?.CallJoinBoard(joinGameKey.text);
                        createGameButton.gameObject.SetActive(false);
                        joinGameButton.gameObject.SetActive(false);
                    }
                });
            }
            else
            {
                createGameButton.gameObject.SetActive(false);
                joinGameButton.gameObject.SetActive(false);
                board?.OnBoardUpdate(Board.Instance.BoardData);
            }

            Board.Instance.OnBoardUpdate += OnBoardUpdate;
            board?.Init();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            board.gameObject.SetActive(true);
            createGameButton.gameObject.SetActive(false);
            joinGameButton.gameObject.SetActive(false);
            board?.OnBoardUpdate(boardData);
        }

        public void DeInit()
        {
            createGameButton?.onClick?.RemoveAllListeners();
            Board.Instance.OnBoardUpdate -= OnBoardUpdate;
            board?.DeInit();
        }
    }
}

