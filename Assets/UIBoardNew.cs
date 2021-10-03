using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Play.Game.Board
{
    public class UIBoardNew : Singleton<UIBoardNew>
    {
        [SerializeField] private Places places = null;

        private GameContent _gameContent;
        private BoardData _boardData;
        private BoardDisplayData _boardDisplayData;
        private Dictionary<int, IBoardPlace> _placesById;

        public void Init()
        {
            _placesById = new Dictionary<int, IBoardPlace>();
        }

        public void DeInit() { }

        public void OnGameContentUpdate(GameContent gameContent)
        {
            _gameContent = gameContent;
            _boardDisplayData = _gameContent?.DisplayData;
            ProcessDisplayData();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            _boardData = boardData;

            var myId = _boardData.MyId;

            foreach (var displayData in _boardDisplayData.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (_placesById.TryGetValue(placeId, out var place))
                {
                    var finalPlaceId = displayData.PlaceId;
                    var cardPlaceDiff = _boardData.Diff.GetDiffForPlace(finalPlaceId);

                    switch (displayData.CardLayoutOption)
                    {
                        case CardLayoutOption.LayedOut:
                            var hand = place as UIHand;
                            var areCardsFaceDown = displayData.CardFaceOption == CardFaceOption.Down;

                            hand?.UpdateWithDiff(cardPlaceDiff, boardData.Me.IsActive, areCardsFaceDown, true);
                            break;
                        case CardLayoutOption.Stacked:
                            var pile = place as UIPile;
                            var cardsCount = _boardData.CardsByPlace.ContainsKey(finalPlaceId) ? _boardData.CardsByPlace[finalPlaceId].Count : 0;
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
            if (_boardDisplayData == null)
                return;

            foreach (var displayData in _boardDisplayData.PlaceDisplayDatas)
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
