using System.Collections.Generic;
using System.Linq;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI
{
    public class UIBoard : Singleton<UIBoard>
    {
        [SerializeField] private Places places = null;

        private GameContent _gameContent;
        private GameDisplay _gameDisplay;
        private GameState _gameState;

        public Dictionary<int, IBoardPlace> _placesById;

        public void Init()
        {
            _placesById = new Dictionary<int, IBoardPlace>();
        }

        public void DeInit()
        {
            foreach (var idPlace in _placesById)
            {
                if (idPlace.Value != null)
                {
                    var monobeh = idPlace.Value as MonoBehaviour;
                    if (monobeh != null)
                        DestroyImmediate(monobeh.gameObject);
                }
            }

            _gameContent = null;
            _gameDisplay = null;
            _gameState = null;
            _placesById = null;
        }

        public void OnGameContentUpdate(GameContent gameContent)
        {
            _gameContent = gameContent;
            ProcessGameContent();
        }

        public void OnGameDisplayUpdate(GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
            ProcessDisplayData();
        }

        public void OnGameStateDiffUpdate(GameState gameState)
        {
            _gameState = gameState;

            foreach (var displayData in _gameDisplay.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;
                IBoardPlace place;

                if (!_placesById.TryGetValue(placeId, out place))
                {
                    place = CreatePlace(displayData, placeId);
                    if (place != null)
                        _placesById[placeId] = place;
                    else
                    {
                        _placesById.Remove(placeId);
                        continue;
                    }
                }

                var placeDiff = _gameState.Diff.GetDiffForPlace(placeId);
                var areCardsFaceDown = (displayData.CardFaceOption == CardFaceOption.Down);

                switch (displayData.CardLayoutOption)
                {
                    case CardLayoutOption.LayedOut:
                        var hand = place as UIHand;
                        var areCardsInteractable = displayData.IsInteractable;
                        hand?.UpdateWithDiff(_gameContent, placeDiff, areCardsInteractable, areCardsFaceDown, true);
                        break;
                    case CardLayoutOption.Stacked:
                        var pile = place as UIPile;
                        var cardsCount = _gameState.CardsByPlace.ContainsKey(placeId) ? _gameState.CardsByPlace[placeId].Count : 0;
                        pile?.UpdateWithDiff(_gameContent, placeDiff, cardsCount, areCardsFaceDown);
                        break;
                    case CardLayoutOption.Widget:
                        var widget = place as UIWidget;
                        var widgetCards = _gameState.GetCardsForPlace(placeId);
                        if (widgetCards == null || widgetCards.Count <= 0)
                        {
                            DestroyImmediate(widget.gameObject);
                            _placesById.Remove(placeId);
                            continue;
                        }
                        widget?.UpdateWithCards(_gameContent, widgetCards);
                        break;
                    case CardLayoutOption.Title:
                        var title = place as UITitle;
                        var titleCards = _gameState.GetCardsForPlace(placeId);
                        if (titleCards == null || titleCards.Count <= 0)
                        {
                            DestroyImmediate(title.gameObject);
                            _placesById.Remove(placeId);
                            continue;
                        }
                        title?.UpdateWithCards(_gameContent, titleCards);
                        break;
                    case CardLayoutOption.Button:
                        var button = place as UIButton;
                        var buttonCards = _gameState.GetCardsForPlace(placeId);
                        if (buttonCards == null || buttonCards.Count <= 0)
                        {
                            DestroyImmediate(button.gameObject);
                            _placesById.Remove(placeId);
                            continue;
                        }
                        button?.UpdateWithCards(_gameContent, buttonCards);
                        break;
                    case CardLayoutOption.Picture:
                        var picture = place as UIPicture;
                        var pictureCards = _gameState.GetCardsForPlace(placeId);
                        if (pictureCards == null || pictureCards.Count <= 0)
                        {
                            DestroyImmediate(picture.gameObject);
                            _placesById.Remove(placeId);
                            continue;
                        }
                        picture?.UpdateWithCards(_gameContent, pictureCards, displayData.Stretch);
                        break;
                }
            }
        }

        private void ProcessGameContent()
        {
            foreach (var place in _placesById.Values)
            {
                if (place != null)
                    place?.UpdateGameContent(_gameContent);
            }
        }

        private void ProcessDisplayData()
        {
            if (_gameDisplay == null)
                return;

            var placeIdsToDelete = _placesById.Keys.Except(_gameDisplay.PlaceDisplayDatas.Select(d => d.PlaceId));

            foreach (var placeIdToDelete in placeIdsToDelete)
            {
                if (_placesById.TryGetValue(placeIdToDelete, out var existingPlace))
                {
                    var monobeh = existingPlace as MonoBehaviour;
                    if (monobeh != null)
                        DestroyImmediate(monobeh.gameObject);
                }
            }

            foreach (var displayData in _gameDisplay.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (_placesById.TryGetValue(placeId, out var existingPlace))
                {
                    var monobeh = existingPlace as MonoBehaviour;
                    if (monobeh != null)
                        DestroyImmediate(monobeh.gameObject);

                    var place = CreatePlace(displayData, placeId);
                    if (place != null)
                        _placesById[placeId] = place;
                    else
                        _placesById.Remove(placeId);
                }
                else
                {
                    var place = CreatePlace(displayData, placeId);
                    if (place != null)
                        _placesById.Add(placeId, place);
                }
            }
        }

        private IBoardPlace CreatePlace(PlaceDisplayData displayData, int placeId)
        {
            if (places.PlacePrefabs.TryGetValue(displayData.CardLayoutOption, out var placePrefab))
            {
                var placeGO = Instantiate(placePrefab, this.transform);
                var place = placeGO.GetComponent<IBoardPlace>();
                place.DisplayData = displayData;

#if UNITY_EDITOR
                placeGO.name = displayData.PlaceName;
#endif
                var placeRect = placeGO.GetComponent<RectTransform>();
                placeRect.anchorMin = new Vector2(displayData.HorizontalAnchors.Min, displayData.VerticalAnchors.Min);
                placeRect.anchorMax = new Vector2(displayData.HorizontalAnchors.Max, displayData.VerticalAnchors.Max);

                return place;
            }
            else
            {
                Debug.Log("no prefab for this place");
                return null;
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
