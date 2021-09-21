namespace Solcery
{
    public static class DisplayDataUtils
    {
        public static int GetFinalPlaceId(PlaceDisplayData placeDisplayData, int playerId)
        {
            return placeDisplayData.Player switch
            {
                PlacePlayer.Player => placeDisplayData.PlaceId + (playerId - 1),
                PlacePlayer.Enemy => placeDisplayData.PlaceId - (playerId - 1),
                _ => placeDisplayData.PlaceId,
            };
        }
    }
}
