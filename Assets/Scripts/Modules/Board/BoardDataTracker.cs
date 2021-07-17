using System.Collections.Generic;
using System.Threading;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.Modules.Board
{
    public class BoardDataTracker : MonoBehaviour
    {
        private BoardData _currentBoardData;
        private BoardData _previousBoardData;

        private CancellationTokenSource _cts;

        private List<BoardDataCardChangedPlace> _cardsThatChangedPlaces;

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
            if (_previousBoardData == null || _currentBoardData == null || _previousBoardData.Cards == null || _currentBoardData.Cards == null)
            {
                _cardsThatChangedPlaces = null;
                return;
            }

            _cardsThatChangedPlaces = new List<BoardDataCardChangedPlace>();

            foreach (var card in _currentBoardData.Cards)
            {
                var cardId = card.CardId;

                var cardInPreviousBoardData = _previousBoardData.GetCard(cardId);
                var cardInCurrentBoardData = _currentBoardData.GetCard(cardId);

                if (cardInPreviousBoardData == null || cardInCurrentBoardData == null)
                    continue;

                var previousPlace = cardInPreviousBoardData.CardPlace;
                var currentPlace = cardInCurrentBoardData.CardPlace;

                if (previousPlace != currentPlace)
                {
                    _cardsThatChangedPlaces.Add(new BoardDataCardChangedPlace()
                    {
                        CardId = cardId,
                        From = previousPlace,
                        To = currentPlace
                    });
                }
            }

            foreach (var change in _cardsThatChangedPlaces) Debug.Log(change.ToString());
        }
    }
}
