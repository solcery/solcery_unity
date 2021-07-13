using Solcery.Ruleset;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplayForPlayer : MonoBehaviour
    {
#pragma warning disable 0414
        [SerializeField] private Toggle visibilityToggle = null;
        [SerializeField] private TMP_Dropdown cardFaceDropdown = null;
        [SerializeField] private TMP_Dropdown cardLayoutDropdown = null;
        [SerializeField] private TMP_InputField minXInput = null;
        [SerializeField] private TMP_InputField maxXInput = null;
        [SerializeField] private TMP_InputField minYInput = null;
        [SerializeField] private TMP_InputField maxYInput = null;
#pragma warning restore 0414

        private PlaceDisplayDataForPlayer _data;

        public void Init(PlaceDisplayDataForPlayer data)
        {
            if (data == null)
            {
                Debug.Log("new PlaceDisplayDataForPlayer");
                data = new PlaceDisplayDataForPlayer();
            }

            _data = data;

            visibilityToggle?.onValueChanged?.AddListener(OnVisibilityChanged);
        }

        private void OnVisibilityChanged(bool isVisible)
        {
            _data.IsVisible = isVisible;
        }
    }
}
