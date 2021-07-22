using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIPlayerHand : UIHand
    {
        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool isPlayer)
        {
            // UnityEngine.Debug.Log("UIPlayerHand UpdateWithDiv");
            base.UpdateWithDiv(cardPlaceDiv, isPlayer, !isPlayer, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardId);
        }
    }
}
