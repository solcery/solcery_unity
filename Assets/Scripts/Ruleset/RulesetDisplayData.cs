using System;
using System.Collections.Generic;
using Solcery.UI.Create;

namespace Solcery.Ruleset
{
    [Serializable]
    public class RulesetDisplayData
    {
        public List<PlayerDisplayData> PlayerDisplayDatas;

        [NonSerialized]
        [Newtonsoft.Json.JsonIgnore]
        private Dictionary<int, UIPlaceDisplayData> _placeDisplayDatas; //key = placeId

        public UIPlaceDisplayData GetDisplayDataByPlaceId(int placeId)
        {
            if (_placeDisplayDatas.TryGetValue(placeId, out var displayData))
                return displayData;

            return null;
        }

        public void Prettify()
        {
            UnityEngine.Debug.Log(PlayerDisplayDatas.Count);
            _placeDisplayDatas = new Dictionary<int, UIPlaceDisplayData>();

            foreach (var playerDisplayData in PlayerDisplayDatas)
            {
                var playerId = playerDisplayData.PlayerId;

                foreach (var placeDisplayDataForPlayer in playerDisplayData.PlaceDisplayData)
                {
                    var placeId = placeDisplayDataForPlayer.PlaceId;

                    UIPlaceDisplayData placeDisplayData;

                    if (!_placeDisplayDatas.ContainsKey(placeId))
                    {
                        placeDisplayData = new UIPlaceDisplayData();
                        _placeDisplayDatas.Add(placeId, placeDisplayData);
                    }
                    else
                    {
                        placeDisplayData = _placeDisplayDatas[placeId];
                    }

                    var displayDataByPlayer = placeDisplayData.DisplayDataByPlayer;

                    UIPlaceDisplayDataForPlayer uiPlaceDisplayDataForPlayer;

                    if (!displayDataByPlayer.ContainsKey(playerId))
                    {
                        uiPlaceDisplayDataForPlayer = new UIPlaceDisplayDataForPlayer(placeDisplayDataForPlayer);
                        displayDataByPlayer.Add(playerId, uiPlaceDisplayDataForPlayer);
                    }
                    else
                    {
                        uiPlaceDisplayDataForPlayer = displayDataByPlayer[playerId];
                    }
                }
            }
        }
    }
}
