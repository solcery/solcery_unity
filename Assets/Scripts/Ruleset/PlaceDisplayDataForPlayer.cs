using System;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlaceDisplayDataForPlayer
    {
        public int PlaceId;
        public bool IsVisible;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VecticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;
    }
}
