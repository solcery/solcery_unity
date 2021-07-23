namespace Solcery
{
    public struct BoardDataCardChangedPlace
    {
        public BoardCardData CardData;
        public CardPlace From;
        public CardPlace To;


        public override string ToString()
        {
            return $"Card with id {CardData.CardId} changed places from {From} to {To}";
        }
    }
}
