using Solcery.Modules.Board;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using Solcery.Utils.Reactives;

namespace Solcery.UI.Play
{
    public class UIPlay : Singleton<UIPlay>
    {
        [SerializeField] private Button createGameButton = null;
        [SerializeField] private Button joinGameButton = null;
        [SerializeField] private TextMeshProUGUI joinGameKey = null;
        [SerializeField] private UIBoard board = null;

        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(Board.Instance?.Tracker?.BoardDataWithDiv, OnBoardUpdate, _cts.Token);
            board?.Init();
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            createGameButton?.onClick?.RemoveAllListeners();
            joinGameButton?.onClick?.RemoveAllListeners();
            board?.DeInit();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            Debug.Log("UIPlay BoardUpdate");
            if (boardData == null)
            {
                Debug.Log("BoardData is null");
                createGameButton?.gameObject?.SetActive(true);
                joinGameButton?.gameObject?.SetActive(true);
                board?.gameObject?.SetActive(false);

                createGameButton?.onClick?.AddListener(OnCreateGameButtonClicked);
                joinGameButton?.onClick?.AddListener(OnJoinGameButtonClicked);
            }
            else
            {
                createGameButton?.gameObject?.SetActive(false);
                joinGameButton?.gameObject?.SetActive(false);
                board?.gameObject?.SetActive(true);

                createGameButton?.onClick?.RemoveAllListeners();
                joinGameButton?.onClick?.RemoveAllListeners();
                board?.OnBoardUpdate(boardData);
            }
        }

        private void OnCreateGameButtonClicked()
        {
            UnityToReact.Instance?.CallCreateBoard();
            createGameButton?.gameObject?.SetActive(false);
            joinGameButton?.gameObject?.SetActive(false);
        }

        private void OnJoinGameButtonClicked()
        {
            if (joinGameKey != null && !string.IsNullOrEmpty(joinGameKey.text))
            {
                UnityToReact.Instance?.CallJoinBoard(joinGameKey.text);
                createGameButton?.gameObject?.SetActive(false);
                joinGameButton?.gameObject?.SetActive(false);
            }
        }
    }
}
