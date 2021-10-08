using System.Collections.Generic;

namespace Solcery
{
    public class GameStateDiff
    {
        public Dictionary<int, CardPlaceDiff> CardPlaceDiffs;

        public CardPlaceDiff GetDiffForPlace(int placeId)
        {
            if (CardPlaceDiffs.TryGetValue(placeId, out var diff))
                return diff;

            return null;
        }

        public GameStateDiff(Dictionary<int, CardPlaceDiff> cardPlaceDiffs)
        {
            CardPlaceDiffs = cardPlaceDiffs;
        }
    }
}
