using UnityEngine;

namespace Solcery.UI.Create
{
    public class UIPlaceDisplay : MonoBehaviour
    {
        private UIPlaceDisplayData _data;

        public void Init(UIPlaceDisplayData data)
        {
            Debug.Log("Init");

            if (data == null)
                data = new UIPlaceDisplayData();

            _data = data;

            Debug.Log(_data.DisplayDataByPlayer.Count);
        }
    }
}
