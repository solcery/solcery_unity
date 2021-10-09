using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI
{
    public class UIBoard : Singleton<UIBoard>
    {
        [SerializeField] private Places places = null;

        private GameDisplay _gameDisplay;
        private GameState _gameState;

        public Dictionary<int, IBoardPlace> _placesById;

        public void Init()
        {
            _placesById = new Dictionary<int, IBoardPlace>();
        }

        public void DeInit() { }

        public void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
            ProcessDisplayData();
        }

        public void OnGameStateUpdate(GameState gameState)
        {
            _gameState = gameState;

            foreach (var displayData in _gameDisplay.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (_placesById.TryGetValue(placeId, out var place))
                {
                    var cardPlaceDiff = _gameState.Diff.GetDiffForPlace(placeId);

                    switch (displayData.CardLayoutOption)
                    {
                        case CardLayoutOption.LayedOut:
                            var hand = place as UIHand;
                            var areCardsFaceDown = (displayData.CardFaceOption == CardFaceOption.Down);
                            var areCardsInteractable = displayData.IsInteractable;
                            hand?.UpdateWithDiff(cardPlaceDiff, areCardsInteractable, areCardsFaceDown, true);
                            break;
                        case CardLayoutOption.Stacked:
                            var pile = place as UIPile;
                            var cardsCount = _gameState.CardsByPlace.ContainsKey(placeId) ? _gameState.CardsByPlace[placeId].Count : 0;
                            pile?.UpdateWithDiff(cardPlaceDiff, cardsCount);
                            break;
                        case CardLayoutOption.Map:
                            break;
                        case CardLayoutOption.Title:
                            break;
                    }
                }
            }

            UICardAnimator.Instance?.LaunchAll();
        }

        private void ProcessDisplayData()
        {
            if (_gameDisplay == null)
                return;

            foreach (var displayData in _gameDisplay.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (_placesById.ContainsKey(placeId))
                {
                    // make sure it is the same place
                    // update cards in place
                }
                else
                {
                    // create a new place
                    if (places.PlacePrefabs.TryGetValue(displayData.CardLayoutOption, out var placePrefab))
                    {
                        var placeGO = Instantiate(placePrefab, this.transform);
                        var place = placeGO.GetComponent<IBoardPlace>();

                        _placesById.Add(placeId, place);
#if UNITY_EDITOR
                        placeGO.name = displayData.PlaceName;
#endif
                        var placeRect = placeGO.GetComponent<RectTransform>();
                        placeRect.anchorMin = new Vector2(displayData.HorizontalAnchors.Min, displayData.VerticalAnchors.Min);
                        placeRect.anchorMax = new Vector2(displayData.HorizontalAnchors.Max, displayData.VerticalAnchors.Max);
                    }
                    else
                    {
                        Debug.Log("no prefab for this place");
                    }
                }
            }
        }

        public bool GetBoardPlace(int placeId, out IBoardPlace place)
        {
            if (_placesById.TryGetValue(placeId, out var boardPlace))
            {
                place = boardPlace;
                return true;
            }

            place = null;
            return false;
        }
    }
}