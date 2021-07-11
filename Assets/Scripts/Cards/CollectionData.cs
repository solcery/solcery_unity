using System;
using System.Collections.Generic;
using Solcery.Ruleset;

namespace Solcery
{
    [Serializable]
    public class CollectionData
    {
        public List<CollectionCardType> CardTypes;
        public RulesetData RulesetData;

        public CollectionData Prettify()
        {
            RulesetData?.Prettify();

            return this;
        }
    }
}
