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
            if (playerIndex >= 0 && boardData.Players.Count > playerIndex)
            {
                _isPlayer = (playerIndex == boardData.MyIndex);
                _isActive = boardData.Players[playerIndex].IsActive;

                UpdatePlayerData(boardData.Players[playerIndex]);
                UpdatePlayerDrawPile(boardData, playerIndex);
                UpdatePlayerHand(boardData, playerIndex);
            }
            else
            {
                UpdatePlayerData(null);
                UpdatePlayerDrawPile(null);
                UpdatePlayerHand(null);
            }
        }

        private void UpdatePlayerData(PlayerData playerData)
        {
            SetHP(playerData != null ? playerData.HP : 0);
            SetCoins(playerData != null ? playerData.Coins : 0);
        }

        private void UpdatePlayerDrawPile(BoardData boardData, int playerIndex = 0)
        {
            if (boardData == null)
                playerDrawPile?.SetCardsCount(0);
            else
            {
                CardPlace drawPile = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);
                playerDrawPile?.SetCardsCount(boardData.Places.ContainsKey(drawPile) ? boardData.Places[drawPile].Count : 0);
            }
        }

        private void UpdatePlayerHand(BoardData boardData, int playerIndex = 0)
        {
            if (boardData == null)
            {
                playerHand?.DeleteAllCards();
                return;
            }

            CardPlace cardPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
            playerHand?.UpdateCards(boardData.Places.ContainsKey(cardPlace) ? boardData.Places[cardPlace] : null, _isPlayer);
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
