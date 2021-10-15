using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery
{
    public class GameStateDiffTracker : Singleton<GameStateDiffTracker>
    {
        // public int StatesProcessed = 0;

        // [HideInInspector]
        // public AsyncReactiveProperty<GameState> GameStateWithDiff;

        // private GameState _newGameState;
        // private GameState _previousGameState;

        // private CancellationTokenSource _cts;

        private List<BoardDataCardChangedPlace> _cardsThatChangedPlaces;
        private List<BoardDataCardChangedPlace> _cardsThatStayed;
        private List<BoardDataCardChangedPlace> _cardsThatGotDeleted;
        private Dictionary<int, CardPlaceDiff> _cardPlaceDiffs;

        public void Init()
        {
            // _cts = new CancellationTokenSource();
            // _previousGameState = null;
            // Reactives.Subscribe(Game.Instance?.GameState, OnGameStateUpdate, _cts.Token);
        }

        public void DeInit()
        {
            // _cts?.Cancel();
            // _cts?.Dispose();
            // _cts = null;

            // _previousGameState = null;
            // _newGameState = null;
        }

        // private GameState OnGameStateUpdate(GameState prevGameState, GameState newGameState)
        // {
        //     _previousGameState = (newGameState == null) ? null : _newGameState;
        //     _newGameState = newGameState;

        //     return TrackCardsThatChangedPlaces(prevGameState, newGameState);
        // }

        public GameState GetGameStateDiff(GameState _previousGameState, GameState _newGameState)
        {
            if (_newGameState == null || _newGameState.Cards == null)
            {
                _cardsThatChangedPlaces = null;
                _cardsThatStayed = null;
                // GameStateWithDiff.Value = null;
                return _newGameState;
            }

            _cardsThatChangedPlaces = new List<BoardDataCardChangedPlace>();
            _cardsThatStayed = new List<BoardDataCardChangedPlace>();
            _cardsThatGotDeleted = new List<BoardDataCardChangedPlace>();

            if (_previousGameState != null)
            {
                foreach(var prevCard in _previousGameState.Cards)
                {
                    if (!_newGameState.CardsById.ContainsKey(prevCard.CardId))
                    {
                        _cardsThatChangedPlaces.Add(new BoardDataCardChangedPlace()
                        {
                            CardData = prevCard,
                            From = _previousGameState?.GetCard(prevCard.CardId)?.CardPlace ?? 0,
                            To = 0
                        });
                    }
                }
            }

            foreach (var card in _newGameState.Cards)
            {
                var cardId = card.CardId;

                var previousPlace = _previousGameState?.GetCard(cardId)?.CardPlace;
                var currentPlace = _newGameState?.GetCard(cardId)?.CardPlace;

                if (previousPlace != currentPlace)
                {
                    _cardsThatChangedPlaces.Add(new BoardDataCardChangedPlace()
                    {
                        CardData = card,
                        From = previousPlace ?? 0,
                        To = currentPlace ?? 0
                    });
                }
                else if (currentPlace != null)
                {
                    _cardsThatStayed.Add(new BoardDataCardChangedPlace()
                    {
                        CardData = card,
                        StayedIn = currentPlace ?? 0
                    });
                }
            }

            _cardPlaceDiffs = new Dictionary<int, CardPlaceDiff>();

            foreach (var change in _cardsThatChangedPlaces)
            {
                if (_cardPlaceDiffs.ContainsKey(change.From))
                    _cardPlaceDiffs[change.From].Departed.Add(change);
                else
                    _cardPlaceDiffs.Add(change.From, new CardPlaceDiff(null, new List<BoardDataCardChangedPlace>() { change }, null));

                if (_cardPlaceDiffs.ContainsKey(change.To))
                    _cardPlaceDiffs[change.To].Arrived.Add(change);
                else
                    _cardPlaceDiffs.Add(change.To, new CardPlaceDiff(null, null, new List<BoardDataCardChangedPlace>() { change }));
            }

            foreach (var stay in _cardsThatStayed)
            {
                if (_cardPlaceDiffs.ContainsKey(stay.StayedIn))
                    _cardPlaceDiffs[stay.StayedIn].Stayed.Add(stay);
                else
                    _cardPlaceDiffs.Add(stay.StayedIn, new CardPlaceDiff(new List<BoardDataCardChangedPlace>() { stay }, null, null));
            }

            _newGameState.Diff = new GameStateDiff(_cardPlaceDiffs);
            // StatesProcessed += 1;
            // GameStateWithDiff.Value = _currentGameState;
            return _newGameState;
        }
    }
}
