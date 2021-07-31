using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceSide : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private PlaceObject parent;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image image;

    public void OnPointerDown(PointerEventData eventData)
    {
        parent.OnSideSelected(rectTransform);
        image.raycastTarget = false;
    }

    public void ResetRayCast()
    {
        image.raycastTarget = true;
    }
}