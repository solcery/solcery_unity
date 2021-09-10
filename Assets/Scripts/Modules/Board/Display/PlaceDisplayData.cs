using System;

namespace Solcery
{
    [Serializable]
    public class PlaceDisplayData
    {
        public bool IsVisible;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VecticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;
    }
}
