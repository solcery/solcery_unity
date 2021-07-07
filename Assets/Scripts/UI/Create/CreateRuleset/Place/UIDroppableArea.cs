using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Create
{
    public class UIDroppableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UIPlaceCard _card;
        private UIDroppableAreaOption _option;
        private Action<UIPlaceCard, UIDroppableAreaOption> _onPointerEnter, _onPointerExit;

        public void Init(UIPlaceCard card, UIDroppableAreaOption option, Action<UIPlaceCard, UIDroppableAreaOption> onPointerEnter, Action<UIPlaceCard, UIDroppableAreaOption> onPointerExit)
        {
            _card = card;
            _option = option;
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter?.Invoke(_card, _option);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit?.Invoke(_card, _option);
        }
    }
}
