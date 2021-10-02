using UnityEngine;

namespace Solcery.UI.NodeEditor
{
    public class UINodeArrow : MonoBehaviour
    {
        public RectTransform rect;
        public RectTransform Line;
        public RectTransform Up;
        public RectTransform Down;

        public ArrowState State;
        public float VOffsetValue = 4f;
        public float Index;

        public void Init(ArrowState state, float vOffset)
        {
            State = state;
            Index = vOffset;

            switch (state)
            {
                case ArrowState.DownIsLeft:
                    Line.gameObject.SetActive(true);
                    Line.offsetMin = new Vector2(-1, -Index * VOffsetValue);
                    Line.offsetMax = new Vector2(1, -Index * VOffsetValue);
                    Line.sizeDelta = new Vector2(Line.sizeDelta.x, 2);

                    Down.sizeDelta = new Vector2(2, Down.sizeDelta.y);
                    Down.anchorMin = new Vector2(0, 0);
                    Down.anchorMax = new Vector2(0, 0.5f);
                    Down.offsetMax = new Vector2(Down.offsetMax.x, -Index * VOffsetValue);

                    Up.sizeDelta = new Vector2(2, Up.sizeDelta.y);
                    Up.anchorMin = new Vector2(1, 0.5f);
                    Up.anchorMax = new Vector2(1, 1);
                    Up.offsetMin = new Vector2(Up.offsetMin.x, -Index * VOffsetValue);

                    break;
                case ArrowState.DownIsRight:
                    Line.gameObject.SetActive(true);
                    Line.offsetMin = new Vector2(-1, -Index * VOffsetValue);
                    Line.offsetMax = new Vector2(1, -Index * VOffsetValue);
                    Line.sizeDelta = new Vector2(Line.sizeDelta.x, 2);

                    Down.sizeDelta = new Vector2(2, Down.sizeDelta.y);
                    Down.anchorMin = new Vector2(1, 0);
                    Down.anchorMax = new Vector2(1, 0.5f);
                    Down.offsetMax = new Vector2(Down.offsetMax.x, -Index * VOffsetValue);


                    Up.sizeDelta = new Vector2(2, Up.sizeDelta.y);
                    Up.anchorMin = new Vector2(0, 0.5f);
                    Up.anchorMax = new Vector2(0, 1);
                    Up.offsetMin = new Vector2(Up.offsetMin.x, -Index * VOffsetValue);
                    break;
                case ArrowState.Equal:
                    Line.gameObject.SetActive(false);
                    rect.sizeDelta = new Vector2(2, rect.sizeDelta.y);
                    rect.localPosition = new Vector2(rect.localPosition.x - 1, rect.localPosition.y);

                    Down.offsetMin = new Vector2(0, 0);
                    Down.offsetMax = new Vector2(0, 0);
                    Down.anchorMin = new Vector2(0, 0);
                    Down.anchorMax = new Vector2(1, 0.5f);

                    Up.offsetMin = new Vector2(0, 0);
                    Up.offsetMax = new Vector2(0, 0);
                    Up.anchorMin = new Vector2(0, 0.5f);
                    Up.anchorMax = new Vector2(1, 1);
                    break;
            }
        }
    }

    public enum ArrowState
    {
        DownIsLeft,
        DownIsRight,
        Equal
    }
}