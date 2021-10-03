using System.Collections.Generic;

namespace Solcery
{
    public class BoardDataDiff
    {
        public Dictionary<int, CardPlaceDiff> CardPlaceDiffs;

        public CardPlaceDiff GetDiffForPlace(int placeId)
        {
            if (CardPlaceDiffs.TryGetValue(placeId, out var diff))
                return diff;

            return null;
        }

        public BoardDataDiff(Dictionary<int, CardPlaceDiff> cardPlaceDiffs)
        {
            CardPlaceDiffs = cardPlaceDiffs;
        }
    }
}
