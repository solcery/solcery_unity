using System.Collections.Generic;

namespace Solcery
{
    public class BoardDataDiv
    {
        public Dictionary<CardPlace, CardPlaceDiv> CardPlaceDivs;

        public BoardDataDiv(Dictionary<CardPlace, CardPlaceDiv> cardPlaceDivs)
        {
            CardPlaceDivs = cardPlaceDivs;
        }
    }
}
