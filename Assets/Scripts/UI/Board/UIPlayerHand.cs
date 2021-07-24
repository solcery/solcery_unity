using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIPlayerHand : UIHand
    {
        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool areCardsInteractable, bool areCardsFaceDown)
        {
            base.UpdateWithDiv(cardPlaceDiv, areCardsInteractable, areCardsFaceDown, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardId);
        }
    }
}
