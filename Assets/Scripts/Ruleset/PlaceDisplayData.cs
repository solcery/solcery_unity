using System;
using System.Collections.Generic;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlaceDisplayData
    {
        public Dictionary<int, PlaceDisplayDataForPlayer> PlaceDisplayDataByPlayer; //0 - for all players
    }
}
