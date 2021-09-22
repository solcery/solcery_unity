namespace Solcery
{
    public static class CardPlaceUtils
    {
        public static int PlayerHandFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => 3,
                1 => 4,
                _ => 0,
            };
        }

        public static int PlayerDrawPileFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => 5,
                1 => 6,
                _ => 0,
            };
        }

        public static int PlayerDiscardPileFromPlayerIndex(int playerIndex)
        {
            return playerIndex switch
            {
                0 => 9,
                1 => 10,
                _ => 0,
            };
        }
    }
}
