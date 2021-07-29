using System.Collections.Generic;
using Solcery.Modules;
using Solcery.WebGL;

namespace Solcery.UI.Play
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
