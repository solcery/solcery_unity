using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.Modules.Board
{
    public class BoardDataTracker : MonoBehaviour
    {
        public AsyncReactiveProperty<BoardData> BoardDataWithDiv;

        private BoardData _currentBoardData;
        private BoardData _previousBoardData;

        private CancellationTokenSource _cts;

        private List<BoardDataCardChangedPlace> _cardsThatChangedPlaces;
        private Dictionary<CardPlace, CardPlaceDiv> _cardPlaceDivs;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            _previousBoardData = _currentBoardData;
            _currentBoardData = boardData;

            TrackCardsThatChangedPlaces();
        }

        private void TrackCardsThatChangedPlaces()
        {
            if (_currentBoardData == null || _currentBoardData.Cards == null)
            {
                _cardsThatChangedPlaces = null;
                BoardDataWithDiv.Value = null;
                return;
            }

            _cardsThatChangedPlaces = new List<BoardDataCardChangedPlace>();

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
            }

            foreach (var change in _cardsThatChangedPlaces) Debug.Log(change.ToString());

            _cardPlaceDivs = new Dictionary<CardPlace, CardPlaceDiv>();

            foreach (var change in _cardsThatChangedPlaces)
            {
                if (_cardPlaceDivs.ContainsKey(change.From))
                    _cardPlaceDivs[change.From].Departed.Add(change);
                else
                    _cardPlaceDivs.Add(change.From, new CardPlaceDiv(new List<BoardDataCardChangedPlace>() { change }, null));

                if (_cardPlaceDivs.ContainsKey(change.To))
                    _cardPlaceDivs[change.To].Arrived.Add(change);
                else
                    _cardPlaceDivs.Add(change.To, new CardPlaceDiv(null, new List<BoardDataCardChangedPlace>() { change }));
            }

            _currentBoardData.Div = new BoardDataDiv(_cardsThatChangedPlaces, _cardPlaceDivs);
            BoardDataWithDiv.Value = _currentBoardData;
        }
    }
}
