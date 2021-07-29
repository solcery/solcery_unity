namespace Solcery.UI.Play
{
    public class UIPlayedThisTurn : UIHand
    {
        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff)
        {
            base.UpdateWithDiff(cardPlaceDiff, false, false, false, true);
        }

        protected override void OnCardCasted(int cardId)
        {
            
        }
    }
}
