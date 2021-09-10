using System;

namespace Solcery
{
    [Serializable]
    public struct PlaceDisplayAnchors
    {
        public float Min;
        public float Max;

        public PlaceDisplayAnchors(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
