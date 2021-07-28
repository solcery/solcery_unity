using System.Collections.Generic;

namespace Solcery
{
    public class CardPlaceDiv
    {
        public List<BoardDataCardChangedPlace> Stayed;
        public List<BoardDataCardChangedPlace> Departed;
        public List<BoardDataCardChangedPlace> Arrived;

        public CardPlaceDiv(List<BoardDataCardChangedPlace> same = null, List<BoardDataCardChangedPlace> departed = null, List<BoardDataCardChangedPlace> arrived = null)
        {
            Stayed = same != null ? same : new List<BoardDataCardChangedPlace>();
            Departed = departed != null ? departed : new List<BoardDataCardChangedPlace>();
            Arrived = arrived != null ? arrived : new List<BoardDataCardChangedPlace>();
        }
    }
}
