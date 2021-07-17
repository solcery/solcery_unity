namespace Solcery
{
    public static class CardPlaceUtils
    {
        public static CardPlace PlayerHandFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => CardPlace.Hand1,
                1 => CardPlace.Hand2,
                _ => CardPlace.Nowhere,
            };
        }

        public static CardPlace PlayerDrawPileFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => CardPlace.DrawPile1,
                1 => CardPlace.DrawPile2,
                _ => CardPlace.DrawPile1,
            };
        }

        public static CardPlace PlayerDiscardPileFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => CardPlace.DiscardPile1,
                1 => CardPlace.DiscardPile2,
                _ => CardPlace.DiscardPile1,
            };
        }
    }
}
