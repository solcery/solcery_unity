using System;

namespace Solcery
{
    [Serializable]
    public class BoardCardType
    {
        public int CardTypeId;
        public string MintAddress;
        public CardMetadata Metadata;
    }
}
