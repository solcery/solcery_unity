using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Play.Game.Board
{
    public class UIBoardNew : Singleton<UIBoardNew>
    {
        [SerializeField] private Places places = null;

        private BoardData _boardData;
        private BoardDisplayData _boardDisplayData;
        private Dictionary<int, IBoardPlace> _placesById;

        public void Init()
        {
            _placesById = new Dictionary<int, IBoardPlace>();
        }

        public void DeInit() { }

        public void OnBoardUpdate(BoardData boardData)
        {
            _boardData = boardData;
            _boardDisplayData = _boardData.DisplayData;

            AssignBoardPlaces();

            var myId = _boardData.MyId;

            foreach (var displayData in _boardDisplayData.PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (_placesById.TryGetValue(placeId, out var place))
                {
                    var finalPlaceId = DisplayDataUtils.GetFinalPlaceId(displayData, myId);

                    switch (displayData.CardLayoutOption)
                    {
                        case CardLayoutOption.LayedOut:
                            var hand = place as UIHand;
                            // var hand = place.GetComponent<UIShop>();
                            hand?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey(finalPlaceId) ? _boardData.Diff.CardPlaceDiffs[finalPlaceId] : null, boardData.Me.IsActive, false, true);
                            break;
                        case CardLayoutOption.Stacked:
                            var pile = place as UIPile;
                            // var pile = placeGO.GetComponent<UIPile>();
                            pile?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey(finalPlaceId) ? _boardData.Diff.CardPlaceDiffs[finalPlaceId] : null, _boardData.CardsByPlace.ContainsKey(finalPlaceId) ? _boardData.CardsByPlace[finalPlaceId].Count : 0);
                            break;
                        case CardLayoutOption.Map:
                            break;
                        case CardLayoutOption.Title:
                            break;
                    }
                }

                //                 if (_placesById.ContainsKey(placeId))
                //                 {
                //                     // make sure it is the same place
                //                     // update cards in place
                //                 }
                //                 else
                //                 {
                //                     // create a new place
                //                     if (places.PlacePrefabs.TryGetValue(displayData.CardLayoutOption, out var placePrefab))
                //                     {
                //                         var placeGO = Instantiate(placePrefab, this.transform);
                //                         var place = placeGO.GetComponent<IBoardPlace>();

                //                         _placesById.Add(placeId, place);
                // #if UNITY_EDITOR
                //                         placeGO.name = displayData.PlaceName;
                // #endif
                //                         var placeRect = placeGO.GetComponent<RectTransform>();
                //                         placeRect.anchorMin = new Vector2(displayData.HorizontalAnchors.Min, displayData.VerticalAnchors.Min);
                //                         placeRect.anchorMax = new Vector2(displayData.HorizontalAnchors.Max, displayData.VerticalAnchors.Max);

                //                         var finalPlaceId = DisplayDataUtils.GetFinalPlaceId(displayData, myId);

                //                         switch (displayData.CardLayoutOption)
                //                         {
                //                             case CardLayoutOption.LayedOut:
                //                                 var hand = placeGO.GetComponent<UIShop>();
                //                                 hand?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey(finalPlaceId) ? _boardData.Diff.CardPlaceDiffs[finalPlaceId] : null, boardData.Me.IsActive, false, true);
                //                                 break;
                //                             case CardLayoutOption.Stacked:
                //                                 var pile = placeGO.GetComponent<UIPile>();
                //                                 pile?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey(finalPlaceId) ? _boardData.Diff.CardPlaceDiffs[finalPlaceId] : null, _boardData.CardsByPlace.ContainsKey(finalPlaceId) ? _boardData.CardsByPlace[finalPlaceId].Count : 0);
                //                                 break;
                //                             case CardLayoutOption.Map:
                //                                 break;
                //                             case CardLayoutOption.Title:
                //                                 break;
                //                         }
                //                     }
                //                     else
                //                     {
                //                         Debug.Log("no prefab for this place");
                //                     }
                // }
            }

            UICardAnimator.Instance?.LaunchAll();
        }

        private void AssignBoardPlaces()
        {
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
