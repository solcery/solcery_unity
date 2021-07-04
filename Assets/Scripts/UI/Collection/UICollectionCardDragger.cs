using System.Collections;
using System.Collections.Generic;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI
{
    public class UICollectionCardDragger : UpdateableBehaviour
    {
        private Canvas _canvas;
        private UICollectionCard _cardClone;

        private Vector2 _movePos;

        public void Init(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void DeInit()
        {

        }

        public void StartDragging(UICollectionCard card)
        {
            _cardClone = Instantiate(card, card.transform.position, Quaternion.identity);
            _cardClone?.DetatchFromGroup();
            _cardClone?.transform.SetParent(_canvas.transform);
            _cardClone?.transform.SetAsLastSibling();
        }

        public override void PerformUpdate()
        {
            if (_cardClone == null) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            Input.mousePosition, _canvas.worldCamera,
            out _movePos);

            _cardClone.transform.position = _canvas.transform.TransformPoint(_movePos);
        }
    }
}
