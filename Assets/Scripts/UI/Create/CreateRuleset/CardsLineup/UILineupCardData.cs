namespace Solcery.UI.Create
{
    public class UILineupCardData
    {
        public CollectionCardType CardType;
        public int Amount;

        public UILineupCardData(CollectionCardType cardType, int amount)
        {
            CardType = cardType;
            Amount = amount;
        }
    }
}
