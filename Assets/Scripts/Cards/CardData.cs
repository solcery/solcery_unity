using System;

namespace Solcery
{
    [Serializable]
    public class CardData
    {
        public int CardId;
        public CardPlace CardPlace;
        public string MintAddress;
        public CardMetadata Metadata;
    }
}
