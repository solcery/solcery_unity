using System;
using System.Collections.Generic;

namespace Solcery.Ruleset
{
    [Serializable]
    public class RulesetData
    {
        public List<string> CardMintAddresses;
        public List<PlaceData> Deck;
        public RulesetDisplayData DisplayData;
    }
}
