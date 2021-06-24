using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIPlayerHand : UIHand
    {
        public void UpdateCards(List<CardData> cards, bool isPlayer, bool isActive)
        {
            base.UpdateCards(cards, areButtonsInteractable: isPlayer);
        }

        protected override void OnCardCasted(string cardMintAddress, int cardIndex)
        {
            UnityToReact.Instance.CallUseCard(cardMintAddress, cardIndex);
        }
    }
}
