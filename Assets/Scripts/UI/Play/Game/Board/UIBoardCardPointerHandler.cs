using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solcery.UI.Play.Game.Board
{
    public class UIBoardCardPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        Action _onPointerEnter, _onPointerExit, _onPointerDown, _onPointerUp, _onDrag;

        public void Init(Action onPointerEnter, Action onPointerExit, Action onPointerDown, Action onPointerUp, Action onDrag)
        {
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
            _onPointerDown = onPointerDown;
            _onPointerUp = onPointerUp;
            _onDrag = onDrag;
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

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _onPointerUp?.Invoke();
        }

        void OnDisable()
        {

        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _onDrag?.Invoke();
        }
    }
}