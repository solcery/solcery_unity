namespace Solcery.UI.Create
{
    public class UIPlaceCardData
    {
        public CollectionCardType CardType;
        public int Amount;

        public UIPlaceCardData(CollectionCardType cardType, int amount)
        {
            CardType = cardType;
            Amount = amount;
        }
    }
}
