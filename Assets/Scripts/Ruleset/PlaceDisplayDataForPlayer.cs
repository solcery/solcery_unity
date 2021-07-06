using System;

namespace Solcery.Ruleset
{
    [Serializable]
    public class PlaceDisplayDataForPlayer
    {
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VecticalAnchors;
        public CardLayoutOption CardLayoutOptionForAllPlayers;
        public CardFaceOption CardFaceOptionForAllPlayers;
    }
}
