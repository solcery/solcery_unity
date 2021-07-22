using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIPlayer : MonoBehaviour
    {
        public UIPlayerHand Hand => hand;
        public UICardsPile DiscardPile => discardPile;
        public UICardsPile DrawPile => drawPile;

        [SerializeField] private UIPlayerHand hand = null;
        [SerializeField] private UICardsPile discardPile = null;
        [SerializeField] private UICardsPile drawPile = null;
        [SerializeField] private TextMeshProUGUI hpText = null;
        [SerializeField] private UIDiv hpDiv = null;
        [SerializeField] private UIDiv coinsDiv = null;
        [SerializeField] private TextMeshProUGUI coinsText = null;

        private bool _isPlayer;
        private bool _isActive;
        private int _currentHP;
        private bool _isInitialHp = true;
        private int _currentCoins;
        private bool _isInitialCoins = true;

        public void OnBoardUpdate(BoardData boardData, int playerIndex)
        {
            if (playerIndex >= 0 && boardData.Players.Count > playerIndex)
            {
                _isPlayer = (playerIndex == boardData.MyIndex);
                _isActive = boardData.Players[playerIndex].IsActive;

                UpdatePlayerData(boardData.Players[playerIndex]);
                UpdatePlayerDrawPile(boardData, playerIndex);
                UpdatePlayerDiscardPile(boardData, playerIndex);
                UpdatePlayerHand(boardData, playerIndex);
            }
            else
            {
                UpdatePlayerData(null);
                UpdatePlayerDrawPile(null);
                UpdatePlayerDiscardPile(null);
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
            CardPlace drawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);

            if (boardData == null)
                this.drawPile?.UpdateWithDiv(boardData.Div.CardPlaceDivs.ContainsKey(drawPilePlace) ? boardData.Div.CardPlaceDivs[drawPilePlace] : null, 0);
            else
            {
                this.drawPile?.UpdateWithDiv(boardData.Div.CardPlaceDivs.ContainsKey(drawPilePlace) ? boardData.Div.CardPlaceDivs[drawPilePlace] : null, boardData.CardsByPlace.ContainsKey(drawPilePlace) ? boardData.CardsByPlace[drawPilePlace].Count : 0);
            }
        }

        private void UpdatePlayerDiscardPile(BoardData boardData, int playerIndex = 0)
        {
            CardPlace discardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(playerIndex);

            if (boardData == null)
                this.discardPile?.UpdateWithDiv(boardData.Div.CardPlaceDivs.ContainsKey(discardPilePlace) ? boardData.Div.CardPlaceDivs[discardPilePlace] : null, 0);
            else
            {

                this.discardPile?.UpdateWithDiv(boardData.Div.CardPlaceDivs.ContainsKey(discardPilePlace) ? boardData.Div.CardPlaceDivs[discardPilePlace] : null, boardData.CardsByPlace.ContainsKey(discardPilePlace) ? boardData.CardsByPlace[discardPilePlace].Count : 0);
            }
        }

        private void UpdatePlayerHand(BoardData boardData, int playerIndex = 0)
        {
            if (boardData == null)
            {
                hand?.DeleteAllCards();
                return;
            }

            CardPlace cardPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
            hand?.UpdateWithDiv(boardData.Div.CardPlaceDivs.ContainsKey(cardPlace) ? boardData.Div.CardPlaceDivs[cardPlace] : null, _isPlayer);
        }

        private void SetHP(int newHP)
        {
            if (_currentHP != newHP)
            {
                if (!_isInitialHp)
                    hpDiv?.Show(newHP - _currentHP);
            }

            _currentHP = newHP;
            _isInitialHp = false;

            if (hpText != null)
                hpText.text = newHP.ToString();
        }

        private void SetCoins(int newCoins)
        {
            if (_currentCoins != newCoins)
            {
                if (!_isInitialCoins)
                    coinsDiv?.Show(newCoins - _currentCoins);
            }

            _currentCoins = newCoins;
            _isInitialCoins = false;

            if (coinsText != null)
                coinsText.text = newCoins.ToString();
        }
    }
}
