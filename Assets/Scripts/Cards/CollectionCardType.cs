using System;

namespace Solcery
{
    [Serializable]
    public class CollectionCardType
    {
        public string MintAddress;
        public CardMetadata Metadata;
        public BrickTree BrickTree;
    }
}
