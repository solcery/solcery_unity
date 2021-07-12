using System.Collections.Generic;
using Solcery.Ruleset;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplayData
    {
        public Dictionary<int, PlaceDisplayDataForPlayer> DisplayDataByPlayer;

        public UIPlaceDisplayData()
        {
            DisplayDataByPlayer = new Dictionary<int, PlaceDisplayDataForPlayer>();
        }
    }
}
