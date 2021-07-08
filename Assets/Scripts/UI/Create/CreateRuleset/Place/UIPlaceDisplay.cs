using System.Collections.Generic;
using Solcery.Ruleset;
using UnityEngine;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplay : MonoBehaviour
    {
        private UIPlaceDisplayData _displayData;

        public void Init()
        {
            _displayData = new UIPlaceDisplayData()
            {
                DisplayDataByPlayer = new Dictionary<int, UIPlaceDisplayDataForPlayer>()
                {
                    {0, new UIPlaceDisplayDataForPlayer()
                    {
                        IsVisible = false,
                        HorizontalAnchors = new PlaceDisplayAnchors(0, 1),
                        VecticalAnchors = new PlaceDisplayAnchors(0, 1),
                        CardFaceOption = CardFaceOption.Up,
                        CardLayoutOption = CardLayoutOption.LayedOut,
                    }}
                }
            };
        }
    }
}
