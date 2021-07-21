namespace Solcery
{
    public struct BoardDataCardChangedPlace
    {
        public int CardId;
        public CardPlace From;
        public CardPlace To;


        public override string ToString()
        {
            return $"Card with id {CardId} changed places from {From} to {To}";
        }
    }
}
