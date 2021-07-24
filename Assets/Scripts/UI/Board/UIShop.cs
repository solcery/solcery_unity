using System.Collections.Generic;
using Solcery.WebGL;

namespace Solcery.UI.Play
{
    public class UIShop : UIHand
    {
        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv, bool areCardsInteractable)
        {
            UnityEngine.Debug.Log($"shop cards interactable: {areCardsInteractable}");
            UnityEngine.Debug.Log(_cardsById.Count);
            base.UpdateWithDiv(cardPlaceDiv, areCardsInteractable, false, true);
        }

        protected override void OnCardCasted(int cardId)
        {
            UnityToReact.Instance.CallUseCard(cardId);
        }
    }
}
