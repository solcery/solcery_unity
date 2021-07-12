using Solcery.Ruleset;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplayDataForPlayer
    {
        public bool IsVisible;
        public PlaceDisplayAnchors HorizontalAnchors;
        public PlaceDisplayAnchors VecticalAnchors;
        public CardFaceOption CardFaceOption;
        public CardLayoutOption CardLayoutOption;

        public UIPlaceDisplayDataForPlayer()
        {
            
        }

        public UIPlaceDisplayDataForPlayer(PlaceDisplayDataForPlayer origin)
        {
            IsVisible = origin.IsVisible;
            HorizontalAnchors = origin.HorizontalAnchors;
            VecticalAnchors = origin.VecticalAnchors;
            CardFaceOption = origin.CardFaceOption;
            CardLayoutOption = origin.CardLayoutOption;
        }
    }
}
