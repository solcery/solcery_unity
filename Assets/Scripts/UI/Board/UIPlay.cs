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
        [SerializeField] private GameObject lookingForOpponent = null;
        [SerializeField] private TextMeshProUGUI joinGameKey = null;
        [SerializeField] private UIBoard board = null;

        private CancellationTokenSource _cts;

        public void Init()
        {
            Debug.Log("Init");
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(BoardDataTracker.Instance?.BoardDataWithDiv, OnBoardUpdate, _cts.Token);
            board?.Init();
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            createGameButton?.onClick?.RemoveAllListeners();
            board?.DeInit();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                games?.SetActive(true);
                createGameButton?.gameObject?.SetActive(true);
                waitingStatus?.SetActive(false);
                lookingForOpponent?.SetActive(false);

                board?.Clear();
                board?.gameObject?.SetActive(false);

                createGameButton?.onClick?.AddListener(OnCreateGameButtonClicked);
            }
            else
            {
                games.SetActive(false);
                createGameButton?.gameObject?.SetActive(false);
                waitingStatus?.SetActive(false);
                lookingForOpponent?.SetActive(false);

                board?.gameObject?.SetActive(true);
                board?.OnBoardUpdate(boardData);

                createGameButton?.onClick?.RemoveAllListeners();
            }
        }

        private void OnCreateGameButtonClicked()
        {
            UnityToReact.Instance?.CallCreateBoard();
            createGameButton?.gameObject?.SetActive(false);
            waitingStatus?.SetActive(true);
            lookingForOpponent?.SetActive(true);
        }

        private void OnJoinGameButtonClicked()
        {
            if (joinGameKey != null && !string.IsNullOrEmpty(joinGameKey.text))
            {
                UnityToReact.Instance?.CallJoinBoard(joinGameKey.text);
                createGameButton?.gameObject?.SetActive(false);
            }
        }
    }
}
