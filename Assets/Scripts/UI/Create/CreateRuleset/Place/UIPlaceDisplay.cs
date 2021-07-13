using UnityEngine;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplay : MonoBehaviour
    {
        public UIPlaceDisplayData Data => _data;

#pragma warning disable 0414
        [SerializeField] private GameObject displayForPlayerPrefab = null;
#pragma warning restore 0414

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
