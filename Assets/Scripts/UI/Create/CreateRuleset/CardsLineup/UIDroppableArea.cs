using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Create
{
    public class UIDroppableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UILineupCard _card;
        private UIDroppableAreaOption _option;
        private Action<UILineupCard> _onPointerEnter, _onPointerExit;

        public void Init(UILineupCard card, UIDroppableAreaOption option, Action<UILineupCard> onPointerEnter, Action<UILineupCard> onPointerExit)
        {
            _card = card;
            _option = option;
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter?.Invoke(_card);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit?.Invoke(_card);
        }
    }
}
