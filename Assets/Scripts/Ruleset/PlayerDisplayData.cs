using System;
using System.Collections.Generic;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlayerDisplayData
    {
        public int PlayerId;
        public List<PlaceDisplayDataForPlayer> PlaceDisplayData;
    }
}
