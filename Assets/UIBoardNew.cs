using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Play.Game.Board
{
    public class UIBoardNew : Singleton<UIBoardNew>
    {
        [SerializeField] private Places places = null;

        private BoardData _boardData;
        private Dictionary<int, IBoardPlace> _placesById;

        public void Init() { }
        public void DeInit() { }

        public void OnBoardUpdate(BoardData boardData)
        {
            Debug.Log("UIBoardNew.OnBoardUpdate");

            _boardData = boardData;

            _placesById = new Dictionary<int, IBoardPlace>();

            var boardDisplayData = boardData.DisplayData;
            var myId = boardData.MyId;

            foreach (var displayData in boardDisplayData.PlaceDisplayDatas)
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

                        var finalPlaceId = DisplayDataUtils.GetFinalPlaceId(displayData, myId);

                        switch (displayData.CardLayoutOption)
                        {
                            case CardLayoutOption.LayedOut:
                                var hand = placeGO.GetComponent<UIHand>();
                                hand?.UpdateWithDiff(_boardData.Diff.CardPlaceDiffs.ContainsKey((CardPlace)finalPlaceId) ? _boardData.Diff.CardPlaceDiffs[(CardPlace)finalPlaceId] : null, boardData.Me.IsActive, false, true);
                                break;
                            case CardLayoutOption.Stacked:
                                break;
                            case CardLayoutOption.Map:
                                break;
                            case CardLayoutOption.Title:
                                break;
                        }
                    }
                    else
                    {
                        Debug.Log("no prefab for this place");
                    }
                }
            }
        }
    }
}
