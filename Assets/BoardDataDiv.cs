using System.Collections.Generic;

namespace Solcery
{
    public class BoardDataDiv
    {
        public List<BoardDataCardChangedPlace> CardsThatChangedPlaces;
        public Dictionary<CardPlace, CardPlaceDiv> CardPlaceDivs;

        public BoardDataDiv(List<BoardDataCardChangedPlace> cardsThatChangedPlaces, Dictionary<CardPlace, CardPlaceDiv> cardPlaceDivs)
        {
            CardsThatChangedPlaces = cardsThatChangedPlaces;
            CardPlaceDivs = cardPlaceDivs;
        }
    }
}
