using System.Collections.Generic;

namespace Solcery.UI.Play
{
    public class UIPlayedThisTurn : UIHand
    {
        // public void UpdateCards(List<BoardCardData> cards, List<BoardCardData> cardsOnTop)
        // {
        //     base.UpdateCards(cards, areButtonsInteractable: false, areCardsFaceDown: false, showCoins: false);

        //     if (cardsOnTop == null || cardsOnTop.Count <= 0) return;

        //     foreach (var cardData in cardsOnTop)
        //     {
        //         UIBoardCard card;

        //         card = Instantiate(cardPrefab, content).GetComponent<UIBoardCard>();
        //         card?.Init(cardData, isInteractable: false, showCoins: false, null);

        //         _cardsById.Add(cardData.CardId, card);
        //     }
        // }

        public void UpdateWithDiv(CardPlaceDiv cardPlaceDiv)
        {
            base.UpdateWithDiv(cardPlaceDiv, false, false, false);
        }

        protected override void OnCardCasted(int cardId)
        {
            
        }
    }
}
