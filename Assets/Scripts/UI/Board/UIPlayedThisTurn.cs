namespace Solcery.UI.Play
{
    public class UIPlayedThisTurn : UIHand
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
