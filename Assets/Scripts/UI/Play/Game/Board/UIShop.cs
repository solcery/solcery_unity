using Solcery.Modules;

namespace Solcery.UI.Play.Game.Board
{
    public class UIShop : UIHand
    {
        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff, bool areCardsInteractable)
        {
            // base.UpdateWithDiff(cardPlaceDiff, areCardsInteractable, false, true);
        }

        protected override void OnCardCasted(int cardId)
        {
            LogActionCreator.Instance?.CastCard(cardId);
        }
    }
}
