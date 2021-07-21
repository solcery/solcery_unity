using System.Collections.Generic;

namespace Solcery.UI.Play
{
    public class UIPlayedThisTurnOnTop : UIHand
    {
        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv)
        {
            base.UpdateWithDiv(cardPlaceDiv, false, false, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            
        }
    }
}
