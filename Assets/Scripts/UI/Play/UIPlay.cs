using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Solcery.Utils.Reactives;
using Solcery.UI.Play.Game.Board;
using Solcery.UI.Play.Game;

namespace Solcery.UI.Play
{
    public class UIPlay : Singleton<UIPlay>
    {
        [SerializeField] private Button summonerGameButton = null;
        [SerializeField] private Button summonerRulesButton = null;
        [SerializeField] private GameObject games = null;
        [SerializeField] private GameObject waitingStatus = null;
        [SerializeField] private GameObject lookingForOpponent = null;
        [SerializeField] private UIBoard board = null;

        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            summonerRulesButton?.onClick.AddListener(() => UIGameRulesPopup.Instance?.Open());
            Reactives.Subscribe(BoardDataDiffTracker.Instance?.BoardDataWithDiff, OnBoardUpdate, _cts.Token);
            board?.Init();
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            summonerGameButton?.onClick?.RemoveAllListeners();
            summonerRulesButton?.onClick?.RemoveAllListeners();
            board?.DeInit();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                // Debug.Log("1");
                games?.SetActive(true);
                summonerGameButton.interactable = true;
                waitingStatus?.SetActive(false);
                lookingForOpponent?.SetActive(false);
                summonerRulesButton?.gameObject.SetActive(false);

                board?.Clear();
                board?.gameObject?.SetActive(false);

                summonerGameButton?.onClick?.AddListener(OnCreateGameButtonClicked);
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                // Debug.Log("2");
                games?.SetActive(true);
                summonerGameButton.interactable = false;
                waitingStatus?.SetActive(true);
                lookingForOpponent?.SetActive(true);
                summonerRulesButton?.gameObject.SetActive(true);

                board?.gameObject?.SetActive(false);
            }
            else
            {
                // Debug.Log("3");
                games.SetActive(false);
                summonerGameButton.interactable = false;
                waitingStatus?.SetActive(false);
                lookingForOpponent?.SetActive(false);
                summonerRulesButton?.gameObject.SetActive(false);

                board?.gameObject?.SetActive(true);
                board?.OnBoardUpdate(boardData);

                summonerGameButton?.onClick?.RemoveAllListeners();
            }
        }

        private void OnCreateGameButtonClicked()
        {
            UnityToReact.Instance?.CallCreateBoard();
            summonerGameButton.interactable = false;
            waitingStatus?.SetActive(true);
            lookingForOpponent?.SetActive(true);
            summonerRulesButton?.gameObject.SetActive(true);
        }
    }
}
