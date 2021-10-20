using System;
using UnityEngine;

namespace Solcery
{
    [Serializable]
    public class PlaceDisplayData
    {
        public string PlaceName;
        public int PlaceId = 3;
        public bool IsInteractable;
        public bool IsVisible;
        public bool Stretch;
        public int ZOrder;
        public bool HasBg;
        public string BgColor;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VerticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;
    }
}
