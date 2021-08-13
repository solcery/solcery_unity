using Solcery.Modules;

namespace Solcery.UI.Play.Game.Board
{
    public class UIPlayerHand : UIHand
    {
        public void UpdateWithDiff(CardPlaceDiff cardPlaceDiff, bool areCardsInteractable, bool areCardsFaceDown)
        {
            base.UpdateWithDiff(cardPlaceDiff, areCardsInteractable, areCardsFaceDown, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            LogActionCreator.Instance?.CastCard(cardId);
        }
    }
}
