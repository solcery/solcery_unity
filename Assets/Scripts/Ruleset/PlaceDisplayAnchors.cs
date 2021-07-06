using System;

namespace Solcery.Ruleset
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
