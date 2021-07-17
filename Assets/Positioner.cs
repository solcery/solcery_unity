using UnityEngine;

public class Positioner : MonoBehaviour
{
    public RectTransform originRect;
    public RectTransform goalRect;

    void Start()
    {
        goalRect.transform.position = originRect.transform.position;
        Debug.Log(goalRect.anchoredPosition);
    }
}
