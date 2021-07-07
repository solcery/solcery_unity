using System.Collections;
using System.Collections.Generic;
using Solcery.UI.Create;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

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
            _cardClone = Instantiate<UICollectionCard>(card, card.transform.position, Quaternion.identity);

            if (_cardClone != null)
            {
                _cardClone.DetatchFromGroup();
                _cardClone.GetComponent<Image>().raycastTarget = false;
                _cardClone.transform.SetParent(_canvas.transform);
                _cardClone.transform.SetAsLastSibling();
            }
        }

        public override void PerformUpdate()
        {
            if (_cardClone == null) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            Input.mousePosition, _canvas.worldCamera,
            out _movePos);

            _cardClone.transform.position = _canvas.transform.TransformPoint(_movePos);

            if (Input.GetMouseButtonUp(0))
            {
                if (UICreateRuleset.Instance?.PlaceUnderPointer != null)
                    UICreateRuleset.Instance?.PlaceUnderPointer.CreateCard(_cardClone.CardType);

                DestroyImmediate(_cardClone.gameObject);
                _cardClone = null;
            }
        }
    }
}
