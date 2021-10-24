using System;

namespace Solcery
{
    [Serializable]
    public struct CardMetadata
    {
        public string PictureUrl;
        public int Picture;
        public CardIcon Icon;
        public string Name;
        public string Description;
    }
}
