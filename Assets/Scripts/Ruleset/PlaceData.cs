using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlaceData
    {
        public List<CardIndexAmount> IndexAmount; //index is index in MintAddresses

        public PlaceData()
        {
            IndexAmount = new List<CardIndexAmount>();
        }
    }
}
