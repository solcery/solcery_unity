using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField] private UIPlayer player = null;
        [SerializeField] private UIPlayer enemy = null;
        [SerializeField] private UIShop shop = null;
        [SerializeField] private UICardsPile deck = null;
        [SerializeField] private UIPlayedThisTurn playedThisTurn = null;
        [SerializeField] private Button endTurnButton = null;

        private BoardData _boardData;

        public void Init()
        {
            endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked());
        }

        public void DeInit()
        {
            endTurnButton?.onClick.RemoveAllListeners();
        }

        public void OnBoardUpdate(BoardDataDiv boardDataDiv)
        {
            var boardData = boardDataDiv.CurrentBoardData;
            _boardData = boardData;

            this.gameObject.SetActive(true);

            player?.OnBoardUpdate(boardData, boardData.MyIndex);
            enemy?.OnBoardUpdate(boardData, boardData.EnemyIndex);

            deck?.SetCardsCount(boardData.CardsByPlace.ContainsKey(CardPlace.Deck) ? boardData.CardsByPlace[CardPlace.Deck].Count : 0);
            shop?.UpdateCards(boardData.CardsByPlace.ContainsKey(CardPlace.Shop) ? boardData.CardsByPlace[CardPlace.Shop] : null);
            playedThisTurn?.UpdateCards(
                boardData.CardsByPlace.ContainsKey(CardPlace.PlayedThisTurn) ? boardData.CardsByPlace[CardPlace.PlayedThisTurn] : null,
                boardData.CardsByPlace.ContainsKey(CardPlace.PlayedThisTurnTop) ? boardData.CardsByPlace[CardPlace.PlayedThisTurnTop] : null
                );

            endTurnButton?.gameObject.SetActive(boardData.Me.IsActive);
            endTurnButton.interactable = boardData.Me.IsActive;
        }

        private void OnEndTurnButtonClicked()
        {
            if (_boardData != null && _boardData.Me != null && _boardData.Me.IsActive)
            {
                UnityToReact.Instance.CallUseCard(_boardData.EndTurnCardId);
                endTurnButton.interactable = false;
            }
        }

        private void AssignPilesToPlaces(BoardData boardData)
        {
            var playerIndex = boardData.MyIndex;
            var playerHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
            var playerDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);
            var playerDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(playerIndex);

            var enemyIndex = boardData.EnemyIndex;
            var enemyHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(enemyIndex);
            var enemyDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(enemyIndex);
            var enemyDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(enemyIndex);
        }
    }
}
