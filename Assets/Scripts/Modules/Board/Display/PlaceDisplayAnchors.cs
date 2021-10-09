using System;

namespace Solcery
{
    [Serializable]
    public struct PlaceDisplayAnchors : IEquatable<PlaceDisplayAnchors>
    {
        public float Min;
        public float Max;

        public PlaceDisplayAnchors(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public override bool Equals(object obj) => obj is PlaceDisplayAnchors other && this.Equals(other);

        public bool Equals(PlaceDisplayAnchors other)
        {
            return Min == other.Min && Max == other.Max;
        }

        public override int GetHashCode() => (Min, Max).GetHashCode();

        public static bool operator ==(PlaceDisplayAnchors lhs, PlaceDisplayAnchors rhs) => lhs.Equals(rhs);
        public static bool operator !=(PlaceDisplayAnchors lhs, PlaceDisplayAnchors rhs) => !(lhs == rhs);
    }
}
