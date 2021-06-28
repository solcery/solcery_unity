using UnityEngine;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINode : MonoBehaviour
    {
        public float BrickWidth;
        public float BrickHeight;
        public float BrickWidthSpacing;
        public float BrickHeightSpacing;

        public RectTransform Image;
        public GameObject ArrowPrefab;
        public UINode[] NodeSlots;
        public UINodeArrow[] Arrows;
        public float Width;
        public float Height;
        public float ChildrenWidth;

        public RectTransform rect;

        public virtual float GetMaxHeight()
        {
            float MaxHeight = BrickHeight;

            if (NodeSlots.Length != 0 && NodeSlots != null)
            {
                var maxSlotHeight = 0f;

                foreach (var slot in NodeSlots)
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

        public virtual float GetMaxWidth()
        {
            var slotsWidth = -BrickWidthSpacing;
            foreach (var slot in NodeSlots)
            {
                if (slot != null)
                {
                    slotsWidth += slot.GetMaxWidth();
                    slotsWidth += BrickWidthSpacing;
                }
            }

            var maxWidth = Mathf.Max(BrickWidth, slotsWidth);
            Width = maxWidth;
            ChildrenWidth = Mathf.Max(0, slotsWidth);

            return maxWidth;
        }

        public virtual void Rebuild()
        {
            var rect = (RectTransform)this.transform;
            rect.sizeDelta = new Vector2(Width, Height);

            var slotsWidthSoFar = Mathf.Max(0, (Width - ChildrenWidth) / 2);
            float halfSlots = NodeSlots.Length / 2f;
            var lastOffset = 0f;
            var hasChangedArrowDirection = false;

            for (int i = 0; i < NodeSlots.Length; i++)
            {
                if (NodeSlots[i] != null)
                {
                    var x = slotsWidthSoFar;
                    NodeSlots[i].transform.localPosition = new Vector2(x, -(BrickHeight + BrickHeightSpacing));

                    if (Arrows[i] == null)
                    {
                        Arrows[i] = Instantiate(ArrowPrefab, transform).GetComponent<UINodeArrow>();
                    }

                    var arrowRect = Arrows[i].GetComponent<RectTransform>();
                    var slotCenterX = x + NodeSlots[i].Width / 2;
                    var brickCenterX = Width / 2;

                    float offset = i + 0.5f - halfSlots;
                    var arrowSlotCenterX = brickCenterX + offset * 80f;
                    arrowRect.transform.localPosition = new Vector2(Mathf.Min(slotCenterX, arrowSlotCenterX), -(BrickHeight));
                    arrowRect.sizeDelta = new Vector2(Mathf.Abs(arrowSlotCenterX - slotCenterX), BrickHeightSpacing + 2);

                    ArrowState arrowState;
                    if (slotCenterX < arrowSlotCenterX)
                    {
                        arrowState = ArrowState.DownIsLeft;
                        lastOffset += 1f;
                    }
                    else if (slotCenterX > arrowSlotCenterX)
                    {
                        if (!hasChangedArrowDirection)
                            hasChangedArrowDirection = true;
                        else
                            lastOffset -= 1f;
                        arrowState = ArrowState.DownIsRight;
                    }
                    else
                        arrowState = ArrowState.Equal;

                    Arrows[i].Init(arrowState, lastOffset);

                    slotsWidthSoFar += NodeSlots[i].Width;
                    slotsWidthSoFar += BrickWidthSpacing;
                    NodeSlots[i].Rebuild();
                }
            }

            slotsWidthSoFar += BrickWidthSpacing;
        }
    }
}