using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI
{
    public class UIBoardCardPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        Action _onPointerEnter, _onPointerExit, _onPointerDown;

        public void Init(Action onPointerEnter, Action onPointerExit, Action onPointerDown)
        {
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
            _onPointerDown = onPointerDown;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _onPointerDown?.Invoke();
        }

        void OnDisable()
        {

        }
    }
}