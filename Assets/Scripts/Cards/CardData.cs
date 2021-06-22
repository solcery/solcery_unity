using System;

namespace Solcery
{
    [Serializable]
    public class CardData
    {
        public int CardIndex;
        public CardPlace CardPlace;
        public string MintAdress;
        public CardMetadata Metadata;
    }
}
