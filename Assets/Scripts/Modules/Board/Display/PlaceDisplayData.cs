using System;

namespace Solcery
{
    [Serializable]
    public class PlaceDisplayData
    {
        public string PlaceName;
        public int PlaceId = 3;
        public PlacePlayer Player;

        // id +- (playerId - 1) * number
        // common = 0, player = 1, enemy = 2
        public bool AreCardsInteractableIfMeIsActive; // bool BehaveAsSummonerShop => cards are interactable if Me is active player
        public bool IsInteractable;
        public bool IsVisible;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VerticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;
    }
}
