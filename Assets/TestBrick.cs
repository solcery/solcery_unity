using System.Collections.Generic;
using UnityEngine;

public class TestBrick : MonoBehaviour
{
    public float BrickWidth = 250;
    public float BrickHeight = 100;
    public float BrickWidthSpacing = 20;
    public float BrickHeightSpacing = 20;

    public RectTransform Image;
    public GameObject ArrowPrefab;

    public Vector2 BrickSize;
    public List<TestBrick> Slots = new List<TestBrick>();
    public float Width;
    public float Height;

    public RectTransform rect;

    private void Awake()
    {
        rect = (RectTransform)transform;
    }

    public float GetMaxHeight()
    {
        float MaxHeight = BrickHeight;

        if (Slots.Count != 0 && Slots != null)
        {
            var maxSlotHeight = 0f;

            foreach (var slot in Slots)
            {
                if (slot != null)
                {
                    var slotHeight = slot.GetMaxHeight();
                    if (slotHeight > maxSlotHeight)
                        maxSlotHeight = slotHeight + BrickHeightSpacing;
                }
            }

            MaxHeight += maxSlotHeight;
        }

        Height = MaxHeight;
        return MaxHeight;
    }

    public float GetMaxWidth()
    {
        var slotsWidth = 0f;
        foreach (var slot in Slots)
        {
            slotsWidth += slot.GetMaxWidth();
            slotsWidth += BrickWidthSpacing;
        }
        slotsWidth += BrickWidthSpacing;

        var maxWidth = Mathf.Max(BrickWidth + BrickWidthSpacing, slotsWidth);
        Width = maxWidth;

        return maxWidth;
    }

    public void PassDown()
    {
        var rect = (RectTransform)this.transform;
        rect.sizeDelta = new Vector2(Width, Height);

        var slotsWidthSoFar = BrickWidthSpacing;

        foreach (var slot in Slots)
        {
            var x = slotsWidthSoFar;
            slot.transform.localPosition = new Vector2(x, -(BrickHeight + BrickHeightSpacing));

            var arrow = Instantiate(ArrowPrefab, transform).GetComponent<TestArrow>();
            var arrowRect = arrow.GetComponent<RectTransform>();
            var slotCenterX = x + slot.Width / 2;
            var brickCenterX = Width / 2;
            arrowRect.transform.localPosition = new Vector2(Mathf.Min(slotCenterX, brickCenterX), -(BrickHeight));
            arrowRect.sizeDelta = new Vector2(Mathf.Abs(brickCenterX - slotCenterX), BrickHeightSpacing);

            ArrowState arrowState;
            if (slotCenterX < brickCenterX)
                arrowState = ArrowState.DownIsLeft;
            else if (slotCenterX > brickCenterX)
                arrowState = ArrowState.DownIsRight;
            else
                arrowState = ArrowState.Equal;
            arrow.Init(arrowState);

            slotsWidthSoFar += slot.Width;
            slotsWidthSoFar += BrickWidthSpacing;
            slot.PassDown();
        }

        slotsWidthSoFar += BrickWidthSpacing;
    }
}
