using System;

namespace Solcery
{
    [Serializable]
    public class CardType
    {
        public int Id;
        public CardMetadata Metadata;
        public BrickTree BrickTree;
    }
}
