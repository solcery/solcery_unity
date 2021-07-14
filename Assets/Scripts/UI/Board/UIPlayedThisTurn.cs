using System.Collections.Generic;

namespace Solcery.UI.Play
{
    public class UIPlayedThisTurn : UIHand
    {
        public void UpdateCards(List<BoardCardData> cards)
        {
            base.UpdateCards(cards, areButtonsInteractable: false, areCardsFaceDown: false, showCoins: false);
        }

        protected override void OnCardCasted(int cardId)
        {

        }
    }
}
