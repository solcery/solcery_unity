using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Create
{
    public class UIDroppableArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UILineupCard _card;
        private UIDroppableAreaOption _option;
        private Action<UILineupCard, UIDroppableAreaOption> _onPointerEnter, _onPointerExit;

        public void Init(UILineupCard card, UIDroppableAreaOption option, Action<UILineupCard, UIDroppableAreaOption> onPointerEnter, Action<UILineupCard, UIDroppableAreaOption> onPointerExit)
        {
            _card = card;
            _option = option;
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("enter");
            _onPointerEnter?.Invoke(_card, _option);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("exit");
            _onPointerExit?.Invoke(_card, _option);
        }
    }
}
