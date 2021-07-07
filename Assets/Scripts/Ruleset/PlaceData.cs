using System;
using System.Collections.Generic;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlaceData
    {
        public int PlaceId;
        public List<CardIndexAmount> IndexAmount; //index is index in MintAddresses

        public PlaceData()
        {
            IndexAmount = new List<CardIndexAmount>();
        }
    }
}
