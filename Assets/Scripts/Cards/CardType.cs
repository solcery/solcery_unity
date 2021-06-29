using System;

namespace Solcery
{
    [Serializable]
    public class CardType
    {
        public int CardTypeId;
        public string MintAddress;
        public CardMetadata Metadata;
    }
}
