using System;

namespace Solcery
{
    [Serializable]
    public class PlaceDisplayData
    {
        public string PlaceName;
        public int PlaceId;
        public bool IsInteractable;
        public int Alpha = 100;
        public bool Stretch;
        public int ZOrder;
        public bool HasBg;
        public string BgColor;
        public int PixelsPerUnit;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VerticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;
    }
}
