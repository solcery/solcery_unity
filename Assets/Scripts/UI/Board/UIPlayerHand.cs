using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIPlayerHand : UIHand
    {
        // public void UpdateCards(List<BoardCardData> cards, bool isPlayer)
        // {
        //     base.UpdateCards(cards, areButtonsInteractable: isPlayer, areCardsFaceDown: !isPlayer, showCoins: false);
        // }

        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool isPlayer)
        {
            base.UpdateWithDiv(cardPlaceDiv, isPlayer, !isPlayer, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardId);
        }
    }
}
