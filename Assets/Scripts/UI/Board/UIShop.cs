using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIShop : UIHand
    {
        public void UpdateCards(List<BoardCardData> cards)
        {
            base.UpdateCards(cards, areButtonsInteractable: true, showCoins: true);
        }

        protected override void OnCardCasted(int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardId);
        }
    }
}
