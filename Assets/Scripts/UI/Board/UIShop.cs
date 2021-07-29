namespace Solcery.UI.Play
{
    public class UIShop : UIHand
    {
        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff, bool areCardsInteractable)
        {
            base.UpdateWithDiff(cardPlaceDiff, areCardsInteractable, false, true);
        }

        protected override void OnCardCasted(int cardId)
        {
            LogActionCreator.CastCard(cardId);
        }
    }
}
