namespace Solcery
{
    public struct BoardDataCardChangedPlace
    {
        public CardData CardData;
        public int From;
        public int To;
        public int StayedIn;


        public override string ToString()
        {
            return $"Card with id {CardData.CardId} changed places from {From} to {To}";
        }
    }
}
