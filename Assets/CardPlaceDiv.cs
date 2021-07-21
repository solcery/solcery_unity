using System.Collections.Generic;
using Solcery.Modules.Board;

namespace Solcery
{
    public class CardPlaceDiv
    {
        public List<BoardDataCardChangedPlace> Departed;
        public List<BoardDataCardChangedPlace> Arrived;

        public CardPlaceDiv(List<BoardDataCardChangedPlace> departed = null, List<BoardDataCardChangedPlace> arrived = null)
        {
            Departed = departed != null ? departed : new List<BoardDataCardChangedPlace>();
            Arrived = arrived != null ? arrived : new List<BoardDataCardChangedPlace>();
        }
    }
}
