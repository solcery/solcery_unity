using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardCardData
    {
        public int CardId;
        public int CardType;
        public int CardPlace;
        public Dictionary<string, int> Attrs;
    }
}
