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

        public override bool Equals(object obj) => this.Equals(obj as PlaceDisplayData);

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

        public static bool operator ==(PlaceDisplayData lhs, PlaceDisplayData rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PlaceDisplayData lhs, PlaceDisplayData rhs) => !(lhs == rhs);
    }
}
