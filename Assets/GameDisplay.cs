using System;
using System.Collections.Generic;
using System.Linq;

namespace Solcery
{
    [Serializable]
    public class GameDisplay
    {
        public List<PlaceDisplayData> PlaceDisplayDatas = new List<PlaceDisplayData>();

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public bool HasBeenProcessed;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, PlaceDisplayData> DisplayDataByPlaceId;

        public GameDisplay Prettify()
        {
            SortPlaceDisplayDatas();
            CreateDisplayDataDictionary();

            return this;
        }

        private void SortPlaceDisplayDatas()
        {
            var sorted = PlaceDisplayDatas.OrderBy(d => d.ZOrder).ToList();
            PlaceDisplayDatas = sorted;
        }

        private void CreateDisplayDataDictionary()
        {
            DisplayDataByPlaceId = new Dictionary<int, PlaceDisplayData>();

            foreach (var displayData in PlaceDisplayDatas)
            {
                var placeId = displayData.PlaceId;

                if (DisplayDataByPlaceId.ContainsKey(placeId))
                    DisplayDataByPlaceId[placeId] = displayData;
                else
                    DisplayDataByPlaceId.Add(placeId, displayData);
            }
        }
    }
}
