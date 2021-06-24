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
            if (boardData.Players.Count > playerIndex)
            {
                _isPlayer = (playerIndex == 0);
                _isActive = boardData.Players[playerIndex].IsActive;

                UpdatePlayerData(boardData.Players[playerIndex]);
                UpdatePlayerDrawPile(boardData);
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

        private void UpdatePlayerDrawPile(BoardData boardData)
        {
            if (boardData == null)
                playerDrawPile?.SetCardsCount(0);
            else
                playerDrawPile?.SetCardsCount(boardData.Places.ContainsKey(CardPlace.DrawPile1) ? boardData.Places[CardPlace.DrawPile1].Count : 0);
        }

        private void UpdatePlayerHand(BoardData boardData, int playerIndex = 0)
        {
            if (boardData == null)
            {
                playerHand?.DeleteAllCards();
                return;
            }
                

            CardPlace cardPlace = playerIndex switch
            {
                0 => CardPlace.Hand1,
                1 => CardPlace.Hand2,
                _ => CardPlace.Nowhere,
            };
            playerHand?.UpdateCards(boardData.Places.ContainsKey(cardPlace) ? boardData.Places[cardPlace] : null, _isPlayer, _isActive);
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
