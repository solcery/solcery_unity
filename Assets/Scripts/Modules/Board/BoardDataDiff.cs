using System.Collections.Generic;

namespace Solcery
{
    public class BoardDataDiff
    {
        public Dictionary<int, CardPlaceDiff> CardPlaceDiffs;

        public BoardDataDiff(Dictionary<int, CardPlaceDiff> cardPlaceDiffs)
        {
            CardPlaceDiffs = cardPlaceDiffs;
        }
    }
}
