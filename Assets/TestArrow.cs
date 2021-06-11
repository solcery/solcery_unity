using UnityEngine;

public class TestArrow : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform Line;
    public RectTransform Up;
    public RectTransform Down;

    public ArrowState State;

    public void Init(ArrowState state)
    {
        State = state;

        switch (state)
        {
            case ArrowState.DownIsLeft:
                Down.anchorMin = new Vector2(0, 0);
                Down.anchorMax = new Vector2(0, 0.5f);
                Up.anchorMin = new Vector2(1, 0.5f);
                Up.anchorMax = new Vector2(1, 1);
                break;
            case ArrowState.DownIsRight:
                Down.anchorMin = new Vector2(1, 0);
                Down.anchorMax = new Vector2(1, 0.5f);
                Up.anchorMin = new Vector2(0, 0.5f);
                Up.anchorMax = new Vector2(0, 1);
                break;
            case ArrowState.Equal:
                Debug.Log("straight arrow");
                Line.gameObject.SetActive(false);
                rect.sizeDelta = new Vector2(3, rect.sizeDelta.y);
                // rect.offsetMin = new Vector2(-1.5f, rect.offsetMin.y);\
                Down.offsetMin = new Vector2(0, 0);
                Down.offsetMax = new Vector2(0, 0);
                Down.anchorMin = new Vector2(0, 0);
                Down.anchorMax = new Vector2(1, 0.5f);
                // Down.offsetMin = Vector2.zero;
                // Down.offsetMax = Vector2.zero;
                // Up.sizeDelta = new Vector2(3, Up.sizeDelta.y);
                Up.offsetMin = new Vector2(0, 0);
                Up.offsetMax = new Vector2(0, 0);
                Up.anchorMin = new Vector2(0, 0.5f);
                Up.anchorMax = new Vector2(1, 1);
                // Up.offsetMin = Vector2.zero;
                // Up.offsetMax = Vector2.zero;

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
