using UnityEngine;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplay : MonoBehaviour
    {
        private UIPlaceDisplayData _data;

        public void Init(UIPlaceDisplayData data)
        {
            if (data == null)
                data = new UIPlaceDisplayData();

            _data = data;
        }
    }
}
