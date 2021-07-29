using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.Modules.Board
{
    public class BoardDataTracker : Singleton<BoardDataTracker>
    {
        [HideInInspector]
        public AsyncReactiveProperty<BoardData> BoardDataWithDiv;

        private BoardData _currentBoardData;
        private BoardData _previousBoardData;

        private CancellationTokenSource _cts;

        private List<BoardDataCardChangedPlace> _cardsThatChangedPlaces;
        private List<BoardDataCardChangedPlace> _cardsThatStayed;
        private Dictionary<CardPlace, CardPlaceDiv> _cardPlaceDivs;

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
                BoardDataWithDiv.Value = null;
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

            if (_currentBoardData.CardsByPlace.TryGetValue(CardPlace.Shop, out var shopCards))
                Debug.Log($"cards in shop: {shopCards.Count}");
            else
                Debug.Log("cards in shop: 0");

            _cardPlaceDivs = new Dictionary<CardPlace, CardPlaceDiv>();

            foreach (var change in _cardsThatChangedPlaces)
            {
                if (_cardPlaceDivs.ContainsKey(change.From))
                    _cardPlaceDivs[change.From].Departed.Add(change);
                else
                    _cardPlaceDivs.Add(change.From, new CardPlaceDiv(null, new List<BoardDataCardChangedPlace>() { change }, null));

                if (_cardPlaceDivs.ContainsKey(change.To))
                    _cardPlaceDivs[change.To].Arrived.Add(change);
                else
                    _cardPlaceDivs.Add(change.To, new CardPlaceDiv(null, null, new List<BoardDataCardChangedPlace>() { change }));
            }

            foreach (var stay in _cardsThatStayed)
            {
                if (_cardPlaceDivs.ContainsKey(stay.StayedIn))
                    _cardPlaceDivs[stay.StayedIn].Stayed.Add(stay);
                else
                    _cardPlaceDivs.Add(stay.StayedIn, new CardPlaceDiv(new List<BoardDataCardChangedPlace>() { stay }, null, null));
            }

            _currentBoardData.Div = new BoardDataDiv(_cardPlaceDivs);
            BoardDataWithDiv.Value = _currentBoardData;
        }
    }
}
