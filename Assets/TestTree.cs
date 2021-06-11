using UnityEngine;

public class TestTree : MonoBehaviour
{
    public TestBrick Genesis;

    public RectTransform rect;

    private void Awake()
    {
        rect = (RectTransform)transform;
    }

    void Start()
    {
        var maxWidth = Genesis.GetMaxWidth();
        var maxHeight = Genesis.GetMaxHeight();

        rect.sizeDelta = new Vector2(maxWidth, maxHeight);
        Genesis.PassDown();
    }
}
