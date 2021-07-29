using System.Collections.Generic;

namespace Solcery.UI.Play
{
    public class UIPlayedThisTurnOnTop : UIHand
    {
        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff)
        {
            base.UpdateWithDiff(cardPlaceDiff, false, false, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            
        }
    }
}
