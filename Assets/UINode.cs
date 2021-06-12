using System.Collections.Generic;
using UnityEngine;

public class UINode : MonoBehaviour
{
    public float BrickWidth = 250;
    public float BrickHeight = 100;
    public float BrickWidthSpacing = 20;
    public float BrickHeightSpacing = 20;

    public RectTransform Image;
    public GameObject ArrowPrefab;
    public UINode[] Slots;
    public TestArrow[] Arrows;
    public float Width;
    public float Height;

    public RectTransform rect;

    // void Awake()
    // {
    //     Arrows = new TestArrow[Slots.Length];
    // }

    public float GetMaxHeight()
    {
        float MaxHeight = BrickHeight;

        if (Slots.Length != 0 && Slots != null)
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
        var slotsWidth = -BrickWidthSpacing;
        foreach (var slot in Slots)
        {
            if (slot != null)
            {
                slotsWidth += slot.GetMaxWidth();
                slotsWidth += BrickWidthSpacing;
            }
        }
        // slotsWidth += BrickWidthSpacing;

        var maxWidth = Mathf.Max(BrickWidth, slotsWidth);
        Width = maxWidth;

        return maxWidth;
    }

    public void Rebuild()
    {
        var rect = (RectTransform)this.transform;
        rect.sizeDelta = new Vector2(Width, Height);

        var slotsWidthSoFar = 0f;

        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null)
            {
                var x = slotsWidthSoFar;
                Slots[i].transform.localPosition = new Vector2(x, -(BrickHeight + BrickHeightSpacing));

                if (Arrows[i] == null)
                {
                    Arrows[i] = Instantiate(ArrowPrefab, transform).GetComponent<TestArrow>();
                }

                var arrowRect = Arrows[i].GetComponent<RectTransform>();
                var slotCenterX = x + Slots[i].Width / 2;
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
                Arrows[i].Init(arrowState);

                slotsWidthSoFar += Slots[i].Width;
                slotsWidthSoFar += BrickWidthSpacing;
                Slots[i].Rebuild();
            }
        }

        slotsWidthSoFar += BrickWidthSpacing;
    }
}
