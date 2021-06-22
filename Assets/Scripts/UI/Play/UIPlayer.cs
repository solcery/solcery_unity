using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private UIPlayerHand playerHand = null;
        [SerializeField] private UIDrawPile playerDrawPile = null;
        [SerializeField] private TextMeshProUGUI hpText = null;
        [SerializeField] private TextMeshProUGUI coinsText = null;

        private bool _isPlayer;
        private bool _isActive;

        public void OnBoardUpdate(BoardData boardData, int playerIndex)
        {
            _isPlayer = (playerIndex == 0);
            _isActive = boardData.Players[playerIndex].IsActive;

            UpdatePlayerData(boardData.Players[playerIndex]);
            UpdatePlayerDrawPile(boardData);
            UpdatePlayerHand(boardData);
        }

        private void UpdatePlayerData(PlayerData playerData)
        {
            SetHP(playerData.HP);
            SetCoins(playerData.Coins);
        }

        private void UpdatePlayerDrawPile(BoardData boardData)
        {
            playerDrawPile?.SetCardsCount(boardData.Places.ContainsKey(CardPlace.DrawPile1) ? boardData.Places[CardPlace.DrawPile1].Count : 0);
        }

        private void UpdatePlayerHand(BoardData boardData)
        {
            playerHand?.UpdateCards(boardData.Places.ContainsKey(CardPlace.Hand1) ? boardData.Places[CardPlace.Hand1] : null, _isPlayer, _isActive);
        }

        private void SetHP(int hp)
        {
            if (hpText != null) hpText.text = hp.ToString();
        }

        private void SetCoins(int coins)
        {
            if (coinsText != null) coinsText.text = coins.ToString();
        }
    }
}
