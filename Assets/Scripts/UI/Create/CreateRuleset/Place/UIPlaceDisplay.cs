using UnityEngine;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplay : MonoBehaviour
    {
        public UIPlaceDisplayData Data => _data;

        [SerializeField] private GameObject displayForPlayerPrefab = null;
        private UIPlaceDisplayData _data;

        public void Init(UIPlaceDisplayData data)
        {
            if (data == null)
            {
                data = new UIPlaceDisplayData();
            }

            _data = data;
        }
    }
}
