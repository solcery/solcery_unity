using System.Collections.Generic;

namespace Solcery
{
    public class BoardDataDiff
    {
        public Dictionary<CardPlace, CardPlaceDiff> CardPlaceDiffs;

        public BoardDataDiff(Dictionary<CardPlace, CardPlaceDiff> cardPlaceDiffs)
        {
            CardPlaceDiffs = cardPlaceDiffs;
        }
    }
}
