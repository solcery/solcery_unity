using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solcery.Ruleset
{
    [Serializable]
    public class RulesetData
    {
        public List<string> CardMintAddresses;
        public List<Vector3Int> Deck;  // (Place, CardTypeId, Amount)
        public List<string> Initializers;
        public RulesetDisplayData DisplayData;
    }
}
