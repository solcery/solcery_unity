using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIShop : UIHand
    {
        public void UpdateCards(List<BoardCardData> cards)
        {
            base.UpdateCards(cards, areButtonsInteractable: true);
        }

        protected override void OnCardCasted(string cardMintAddress, int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardMintAddress, cardId);
        }
    }
}
