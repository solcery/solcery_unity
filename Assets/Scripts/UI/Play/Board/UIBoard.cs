using System.Collections.Generic;
using Solcery.Modules;
using Solcery.Utils;
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
        private Dictionary<CardPlace, IBoardPlace> _boardPlaces;

        public void Init()
        {
            endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked());
        }

        public void DeInit()
        {
            endTurnButton?.onClick.RemoveAllListeners();
        }

        public void Clear()
        {
            _boardData = null;
            _boardPlaces = null;

            player?.Clear();
            enemy?.Clear();
            deck?.Clear();
            shop?.Clear();
            playedThisTurn?.Clear();
            playedThisTurnOnTop?.Clear();

            UICardAnimator.Instance?.Clear();
            endTurnButton?.gameObject?.SetActive(false);
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            _boardData = boardData;
            AssignBoardPlaces(_boardData);

            player?.OnBoardUpdate(_boardData, _boardData.MyIndex);
            enemy?.OnBoardUpdate(_boardData, _boardData.EnemyIndex);

            deck?.UpdateWithDiff(
                _boardData.Diff.CardPlaceDiffs.ContainsKey(CardPlace.Deck) ? _boardData.Diff.CardPlaceDiffs[CardPlace.Deck] : null,
                _boardData.CardsByPlace.ContainsKey(CardPlace.Deck) ? _boardData.CardsByPlace[CardPlace.Deck].Count : 0
                );

            shop?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey(CardPlace.Shop) ? _boardData.Diff.CardPlaceDiffs[CardPlace.Shop] : null, boardData.Me.IsActive);

            if (_boardData.Diff.CardPlaceDiffs.ContainsKey(CardPlace.PlayedThisTurn))
                playedThisTurn?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs[CardPlace.PlayedThisTurn]);
            if (_boardData.Diff.CardPlaceDiffs.ContainsKey(CardPlace.PlayedThisTurnTop))
                playedThisTurnOnTop?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs[CardPlace.PlayedThisTurnTop]);
            UICardAnimator.Instance?.LaunchAll();

            endTurnButton?.gameObject.SetActive(_boardData.Me.IsActive);
            endTurnButton.interactable = _boardData.Me.IsActive;
        }

        private void OnEndTurnButtonClicked()
        {
            if (_boardData != null && _boardData.Me != null && _boardData.Me.IsActive)
            {
                LogActionCreator.Instance?.CastCard(_boardData.EndTurnCardId);
                endTurnButton.interactable = false;
            }
        }

        private void AssignBoardPlaces(BoardData boardData)
        {
            var playerIndex = boardData.MyIndex;
            var enemyIndex = boardData.EnemyIndex;

            _boardPlaces = new Dictionary<CardPlace, IBoardPlace>()
            {
                { CardPlace.Deck, deck },
                { CardPlace.Shop, shop },

                { CardPlace.PlayedThisTurn, playedThisTurn },
                { CardPlace.PlayedThisTurnTop, playedThisTurnOnTop },
            };

            if (playerIndex >= 0)
            {
                var playerHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(playerIndex);
                var playerDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(playerIndex);
                var playerDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(playerIndex);

                _boardPlaces.Add(playerHandPlace, player.Hand);
                _boardPlaces.Add(playerDrawPilePlace, player.DrawPile);
                _boardPlaces.Add(playerDiscardPilePlace, player.DiscardPile);
            }

            if (enemyIndex >= 0)
            {
                var enemyHandPlace = CardPlaceUtils.PlayerHandFromPlayerIndex(enemyIndex);
                var enemyDrawPilePlace = CardPlaceUtils.PlayerDrawPileFromPlayerIndex(enemyIndex);
                var enemyDiscardPilePlace = CardPlaceUtils.PlayerDiscardPileFromPlayerIndex(enemyIndex);

                _boardPlaces.Add(enemyHandPlace, enemy.Hand);
                _boardPlaces.Add(enemyDrawPilePlace, enemy.DrawPile);
                _boardPlaces.Add(enemyDiscardPilePlace, enemy.DiscardPile);
            }
        }

        public bool GetBoardPlace(CardPlace cardPlace, out IBoardPlace place)
        {
            if (_boardPlaces.TryGetValue(cardPlace, out var boardPlace))
            {
                place = boardPlace;
                return true;
            }

            place = null;
            return false;
        }
    }
}
