using System;

namespace Solcery
{
    [Serializable]
    public struct CardMetadata
    {
        public string PictureUrl;
        public int Picture;
        public CardIcon Icon;
        public int Coins;
        public string Name;
        public string Description;
    }
}
