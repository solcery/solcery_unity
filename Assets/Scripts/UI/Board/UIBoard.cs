using System.Collections.Generic;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public class UIBoard : Singleton<UIBoard>
    {
        [SerializeField] private UIPlayer player = null;
        [SerializeField] private UIPlayer enemy = null;
        [SerializeField] private UIShop shop = null;
        [SerializeField] private UIPile deck = null;
        [SerializeField] private UIPlayedThisTurn playedThisTurn = null;
        [SerializeField] private UIPlayedThisTurnOnTop playedThisTurnOnTop = null;
        [SerializeField] private Button endTurnButton = null;

        private BoardData _boardData;
        private Dictionary<CardPlace, IBoardPlace> BoardPlaces;

        public void Init()
        {
            endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked());
        }

        public void DeInit()
        {
            endTurnButton?.onClick.RemoveAllListeners();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            Debug.Log("UIBoard OnBoardUpdate");
            _boardData = boardData;
            Debug.Log("1");
            AssignBoardPlaces(_boardData);
            Debug.Log("2");

            this.gameObject.SetActive(true);

            player?.OnBoardUpdate(_boardData, _boardData.MyIndex);
            Debug.Log("3");

            enemy?.OnBoardUpdate(_boardData, _boardData.EnemyIndex);
            Debug.Log("4");

            // deck?.SetCardsCount(_boardData.CardsByPlace.ContainsKey(CardPlace.Deck) ? _boardData.CardsByPlace[CardPlace.Deck].Count : 0);
            deck?.UpdateWithDiv(
                _boardData.Div.CardPlaceDivs.ContainsKey(CardPlace.Deck) ? _boardData.Div.CardPlaceDivs[CardPlace.Deck] : null,
                _boardData.CardsByPlace.ContainsKey(CardPlace.Deck) ? _boardData.CardsByPlace[CardPlace.Deck].Count : 0
                );
            Debug.Log("5");

            shop?.UpdateWithDiv(_boardData.Div.CardPlaceDivs.ContainsKey(CardPlace.Shop) ? _boardData.Div.CardPlaceDivs[CardPlace.Shop] : null);
            playedThisTurn?.UpdateWithDiv(_boardData.Div.CardPlaceDivs.ContainsKey(CardPlace.PlayedThisTurn) ? _boardData.Div.CardPlaceDivs[CardPlace.PlayedThisTurn] : null);
            playedThisTurnOnTop?.UpdateWithDiv(_boardData.Div.CardPlaceDivs.ContainsKey(CardPlace.PlayedThisTurnTop) ? _boardData.Div.CardPlaceDivs[CardPlace.PlayedThisTurnTop] : null);
            UICardAnimator.Instance?.LaunchAll();
            Debug.Log("6");

            endTurnButton?.gameObject.SetActive(_boardData.Me.IsActive);
            endTurnButton.interactable = _boardData.Me.IsActive;
        }

        private void OnEndTurnButtonClicked()
        {
            if (_boardData != null && _boardData.Me != null && _boardData.Me.IsActive)
            {
                UnityToReact.Instance.CallUseCard(_boardData.EndTurnCardId);
                endTurnButton.interactable = false;
            }
        }

        private void AssignBoardPlaces(BoardData boardData)
        {
            var playerIndex = boardData.MyIndex;
            Debug.Log($"playerIndex: {playerIndex}");
            // var playerHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
            // var playerDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);
            // var playerDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(playerIndex);

            var enemyIndex = boardData.EnemyIndex;
            Debug.Log($"enemyIndex: {enemyIndex}");
            // var enemyHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(enemyIndex);
            // var enemyDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(enemyIndex);
            // var enemyDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(enemyIndex);

            BoardPlaces = new Dictionary<CardPlace, IBoardPlace>()
            {
                { CardPlace.Deck, deck },
                { CardPlace.Shop, shop },

                // { playerHandPlace, player.Hand },
                // { enemyHandPlace, enemy.Hand },

                // { playerDrawPilePlace, player.DrawPile },
                // { enemyDrawPilePlace, enemy.DrawPile },

                { CardPlace.PlayedThisTurn, playedThisTurn },
                { CardPlace.PlayedThisTurnTop, playedThisTurnOnTop },

                // { playerDiscardPilePlace, player.DiscardPile },
                // { enemyDiscardPilePlace, enemy.DiscardPile },
            };

            if (playerIndex >= 0)
            {
                var playerHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
                var playerDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);
                var playerDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(playerIndex);

                BoardPlaces.Add(playerHandPlace, player.Hand);
                BoardPlaces.Add(playerDrawPilePlace, player.DrawPile);
                BoardPlaces.Add(playerDiscardPilePlace, player.DiscardPile);
            }

            if (enemyIndex >= 0)
            {
                var enemyHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(enemyIndex);
                var enemyDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(enemyIndex);
                var enemyDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(enemyIndex);

                BoardPlaces.Add(enemyHandPlace, enemy.Hand);
                BoardPlaces.Add(enemyDrawPilePlace, enemy.DrawPile);
                BoardPlaces.Add(enemyDiscardPilePlace, enemy.DiscardPile);
            }
        }

        public bool GetBoardPlace(CardPlace cardPlace, out IBoardPlace place)
        {
            if (BoardPlaces.TryGetValue(cardPlace, out var boardPlace))
            {
                place = boardPlace;
                return true;
            }

            place = null;
            return false;
        }
    }
}
