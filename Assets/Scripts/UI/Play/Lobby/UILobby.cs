using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play.Lobby
{
    public class UILobby : Singleton<UILobby>
    {
        [SerializeField] private Button summonerGameButton = null;
        [SerializeField] private Button summonerRulesButton = null;
        [SerializeField] private GameObject waitingStatus = null;
        [SerializeField] private GameObject lookingForOpponent = null;

        public void Init()
        {
            summonerRulesButton?.onClick.AddListener(() => UIGameRulesPopup.Instance?.Open());
        }

        public void DeInit()
        {
            summonerRulesButton?.onClick?.RemoveAllListeners();
            summonerGameButton?.onClick?.RemoveAllListeners();
        }

        public void NotInGame()
        {
            summonerGameButton.interactable = true;
            waitingStatus?.SetActive(false);
            lookingForOpponent?.SetActive(false);
            summonerRulesButton?.gameObject.SetActive(false);

            summonerGameButton?.onClick?.AddListener(OnCreateGameButtonClicked);
        }

        public void WaitingForOpponent()
        {
            summonerGameButton.interactable = false;
            waitingStatus?.SetActive(true);
            lookingForOpponent?.SetActive(true);
            summonerRulesButton?.gameObject.SetActive(true);
        }

        private void OnCreateGameButtonClicked()
        {
            // OldUnityToReact.Instance?.CallCreateBoard();

            summonerGameButton.interactable = false;
            waitingStatus?.SetActive(true);
            lookingForOpponent?.SetActive(true);
            summonerRulesButton?.gameObject.SetActive(true);
        }
    }
}
