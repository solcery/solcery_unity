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
        [SerializeField] private GameObject games = null;
        [SerializeField] private GameObject waitingStatus = null;
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

        private void OnBoardUpdate(BoardData boardData)
        {
            Debug.Log("UIPlay OnBoardUpdate");

            if (boardData == null)
            {
                games?.SetActive(true);
                createGameButton?.gameObject?.SetActive(true);
                waitingStatus?.SetActive(false);
                // joinGameButton?.gameObject?.SetActive(true);
                board?.gameObject?.SetActive(false);

                createGameButton?.onClick?.AddListener(OnCreateGameButtonClicked);
                // joinGameButton?.onClick?.AddListener(OnJoinGameButtonClicked);
            }
            else
            {
                games.SetActive(false);
                createGameButton?.gameObject?.SetActive(false);
                waitingStatus?.SetActive(false);
                // joinGameButton?.gameObject?.SetActive(false);
                board?.gameObject?.SetActive(true);

                createGameButton?.onClick?.RemoveAllListeners();
                // joinGameButton?.onClick?.RemoveAllListeners();
                board?.OnBoardUpdate(boardData);
            }
        }

        private void OnCreateGameButtonClicked()
        {
            UnityToReact.Instance?.CallCreateBoard();
            createGameButton?.gameObject?.SetActive(false);
            waitingStatus?.SetActive(true);
            // joinGameButton?.gameObject?.SetActive(false);
        }

        private void OnJoinGameButtonClicked()
        {
            if (joinGameKey != null && !string.IsNullOrEmpty(joinGameKey.text))
            {
                UnityToReact.Instance?.CallJoinBoard(joinGameKey.text);
                createGameButton?.gameObject?.SetActive(false);
                // joinGameButton?.gameObject?.SetActive(false);
            }
        }
    }
}
