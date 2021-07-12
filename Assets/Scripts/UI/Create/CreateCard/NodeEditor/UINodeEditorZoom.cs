using Solcery.Utils;
using UnityEngine;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINodeEditorZoom : UpdateableBehaviour
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private UINodeEditorInput input = null;

        private float _scrollDelta;
        private bool _isActive;

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public override void PerformUpdate()
        {
            if (!_isActive)
                return;

            if (input.IsMouseOver)
            {
                _scrollDelta = Input.mouseScrollDelta.y;

                if (_scrollDelta != 0)
                {
                    var currentScale = content.localScale;
                    content.localScale = new Vector2(Mathf.Clamp(currentScale.x + _scrollDelta * scrollSpeed, minZoom, maxZoom), Mathf.Clamp(currentScale.y + _scrollDelta * scrollSpeed, minZoom, maxZoom));
                }
            }
        }
    }
}
