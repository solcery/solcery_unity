using UnityEngine;

public class TestTree : MonoBehaviour
{
    public GameObject TestBrickPrefab;
    public TestBrick Genesis;

    public RectTransform rect;

    void Start()
    {
        // var firstBrick = Instantiate(TestBrickPrefab, transform);
        Rebuild();
    }

    public void Rebuild()
    {
        var maxWidth = Genesis.GetMaxWidth();
        var maxHeight = Genesis.GetMaxHeight();

        rect.sizeDelta = new Vector2(maxWidth, maxHeight);
        Genesis.Rebuild();
    }
}
