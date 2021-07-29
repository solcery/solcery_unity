using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.Modules.Board
{
    public class BoardDataDiffTracker : Singleton<BoardDataDiffTracker>
    {
        [HideInInspector]
        public AsyncReactiveProperty<BoardData> BoardDataWithDiff;

        private BoardData _currentBoardData;
        private BoardData _previousBoardData;

        private CancellationTokenSource _cts;

        private List<BoardDataCardChangedPlace> _cardsThatChangedPlaces;
        private List<BoardDataCardChangedPlace> _cardsThatStayed;
        private Dictionary<CardPlace, CardPlaceDiff> _cardPlaceDiffs;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            _previousBoardData = null;
            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();

            _previousBoardData = null;
            _currentBoardData = null;
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            Debug.Log("BoardDataTracker.OnBoardUpdate");

            _previousBoardData = _currentBoardData;
            _currentBoardData = boardData;

            TrackCardsThatChangedPlaces();
        }

        private void TrackCardsThatChangedPlaces()
        {
            if (_currentBoardData == null || _currentBoardData.Cards == null)
            {
                _cardsThatChangedPlaces = null;
                _cardsThatStayed = null;
                BoardDataWithDiff.Value = null;
                return;
            }

            _cardsThatChangedPlaces = new List<BoardDataCardChangedPlace>();
            _cardsThatStayed = new List<BoardDataCardChangedPlace>();

            foreach (var card in _currentBoardData.Cards)
            {
                var cardId = card.CardId;

                var previousPlace = _previousBoardData?.GetCard(cardId)?.CardPlace;
                var currentPlace = _currentBoardData?.GetCard(cardId)?.CardPlace;

                if (previousPlace != currentPlace)
                {
                    _cardsThatChangedPlaces.Add(new BoardDataCardChangedPlace()
                    {
                        CardData = card,
                        From = previousPlace ?? CardPlace.Nowhere,
                        To = currentPlace ?? CardPlace.Nowhere
                    });
                }
                else if (currentPlace != null)
                {
                    _cardsThatStayed.Add(new BoardDataCardChangedPlace()
                    {
                        CardData = card,
                        StayedIn = currentPlace ?? CardPlace.Nowhere
                    });
                }
            }

            _cardPlaceDiffs = new Dictionary<CardPlace, CardPlaceDiff>();

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

            _currentBoardData.Diff = new BoardDataDiff(_cardPlaceDiffs);
            BoardDataWithDiff.Value = _currentBoardData;
        }
    }
}
