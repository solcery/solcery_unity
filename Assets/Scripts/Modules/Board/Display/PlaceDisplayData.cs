using System;

namespace Solcery
{
    [Serializable]
    public class PlaceDisplayData : IEquatable<PlaceDisplayData>
    {
        public string PlaceName;
        public int PlaceId = 3;
        public bool IsInteractable;
        public bool IsVisible;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VerticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;

        public bool Equals(PlaceDisplayData other)
        {
            if (other is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return PlaceName == other.PlaceName &&
            PlaceId == other.PlaceId &&
            IsInteractable == other.IsInteractable &&
            IsVisible == other.IsVisible &&
            HorizontalAnchors == other.HorizontalAnchors &&
            VerticalAnchors == other.VerticalAnchors &&
            CardFaceOption == other.CardFaceOption &&
            CardLayoutOption == other.CardLayoutOption;
        }
    }
}
