using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UIBrickObjectSwitcher : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectName = null;
        [SerializeField] private Button prevObjButton = null;
        [SerializeField] private Button nextObjButton = null;

        private BrickData _data;
        private int _currentIndex = 0;
        private int _maxIndex;

        public void Init(BrickData data)
        {
            _data = data;

            _maxIndex = Enum.GetNames(typeof(BrickObject)).Length - 1;
            objectName.text = Enum.GetName(typeof(BrickObject), 0);

            prevObjButton.onClick.AddListener(Prev);
            nextObjButton.onClick.AddListener(Next);

            ApplyIndex();
        }

        private void ApplyIndex()
        {
            objectName.text = Enum.GetName(typeof(BrickObject), _currentIndex);
            _data.Object = _currentIndex;
        }

        private void Prev()
        {
            if (Enum.IsDefined(typeof(BrickObject), _currentIndex - 1))
                _currentIndex -= 1;
            else
                _currentIndex = _maxIndex;

            ApplyIndex();
        }

        private void Next()
        {
            if (Enum.IsDefined(typeof(BrickObject), _currentIndex + 1))
                _currentIndex += 1;
            else
                _currentIndex = 0;

            ApplyIndex();
        }
    }
}
