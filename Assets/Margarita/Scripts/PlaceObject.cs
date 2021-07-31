using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IEndDragHandler, IDeselectHandler
{
    [Header("Components"), SerializeField]
    private RectTransform rectTransform;

    [Header("Resize Points"), SerializeField]
    private RectTransform upperLeft;
    [SerializeField]
    private RectTransform upperRight;
    [SerializeField]
    private RectTransform bottomLeft;
    [SerializeField]
    private RectTransform bottomRight;
    [Header("Visual"), SerializeField]
    private GameObject resizePoints;

    [Header("Sides"), SerializeField]
    private RectTransform rightSide;
    [SerializeField]
    private RectTransform leftSide;
    [SerializeField]
    private RectTransform upperSide;
    [SerializeField]
    private RectTransform bottomSide;
    [Header("Visual"), SerializeField]
    private GameObject sides;

    private List<RectTransform> resizePointsList = new List<RectTransform>();
    private List<PlaceSide> sidesList = new List<PlaceSide>();

    private RectTransform currentResizePoint;
    private RectTransform currentSide;

    private PlaceInfo currentInfo;

    private PlacesController placesController;

    private bool isDrag;

    private Vector2 mousePos;

    private void Start()
    {
        resizePointsList.AddRange(new List<RectTransform>() { upperLeft, upperRight, bottomLeft, bottomRight });
        sidesList.AddRange(new List<PlaceSide>() { upperSide.GetComponent<PlaceSide>(),
                                                   bottomSide.GetComponent<PlaceSide>(),
                                                   rightSide.GetComponent<PlaceSide>(),
                                                   leftSide.GetComponent<PlaceSide>() });
        placesController = FindObjectOfType<PlacesController>();
        isDrag = false;

        SetCenterAnchorPreset();
    }

    #region Custom

    public void Set(PlaceInfo info)
    {
        currentInfo = info;
    }

    public void OnSideSelected(RectTransform selectedSide)
    {
        currentSide = selectedSide;
    }

    public void ResetPointsAndSides()
    {
        currentSide = null;
        currentResizePoint = null;
    }

    private void SetPivotDependOnCurrentResizePoint()
    {
        #region Corners

        if (currentResizePoint != null)
        {
            if (currentResizePoint == upperLeft)
            {
                SetPivot(new Vector2(1, 0));
            }
            else if (currentResizePoint == upperRight)
            {
                SetPivot(new Vector2(0, 0));
            }
            else if (currentResizePoint == bottomLeft)
            {
                SetPivot(new Vector2(1, 1));
            }
            else if (currentResizePoint == bottomRight)
            {
                SetPivot(new Vector2(0, 1));
            }
        }

        #endregion
        #region Sides

        else if (currentSide != null)
        {
            if (currentSide == leftSide)
            {
                SetPivot(new Vector2(1, 0.5f));
            }
            else if (currentSide == rightSide)
            {
                SetPivot(new Vector2(0, 0.5f));
            }
            else if (currentSide == upperSide)
            {
                SetPivot(new Vector2(0.5f, 0));
            }
            else if (currentSide == bottomSide)
            {
                SetPivot(new Vector2(0.5f, 1));
            }
        }

        #endregion
    }

    private void Flip()
    {
        if (rectTransform.sizeDelta.x < 0)
        {
            rectTransform.sizeDelta = new Vector2(-rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);

            if (rectTransform.pivot.x == 0)
            {
                SetPivot(new Vector2(1, rectTransform.pivot.y));
            }
            else if (rectTransform.pivot.x == 1)
            {
                SetPivot(new Vector2(0, rectTransform.pivot.y));
            }
        }
        if (rectTransform.sizeDelta.y < 0)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, -rectTransform.sizeDelta.y);

            if (rectTransform.pivot.y == 0)
            {
                SetPivot(new Vector2(rectTransform.pivot.x, 1));
            }
            else if (rectTransform.pivot.y == 1)
            {
                SetPivot(new Vector2(rectTransform.pivot.x, 0));
            }
        }
    }

    private void SetCenterAnchorPreset()
    {
        Vector2 size = rectTransform.rect.size;

        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        rectTransform.sizeDelta = size;
    }

    private void SetStrectAnchorPreset()
    {
        var size = rectTransform.rect.size;

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);

        Vector2 deltaSize = size - rectTransform.rect.size;
        rectTransform.offsetMin = rectTransform.offsetMin - new Vector2(deltaSize.x * rectTransform.pivot.x, deltaSize.y * rectTransform.pivot.y);
        rectTransform.offsetMax = rectTransform.offsetMax + new Vector2(deltaSize.x * (1f - rectTransform.pivot.x), deltaSize.y * (1f - rectTransform.pivot.y));
    }

    private void SetPivot(Vector2 pivot)
    {
        Vector2 size = rectTransform.rect.size;
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
    }

    private void ResizeDependOnPivot(Vector2 resizeValue)
    {
        #region If drag by corners

        if (currentResizePoint != null)
        {
            //Bottom Right
            if (rectTransform.pivot == new Vector2(0, 1))
            {
                rectTransform.sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
            }
            //Bottom Left
            else if (rectTransform.pivot == new Vector2(1, 1))
            {
                rectTransform.sizeDelta += new Vector2(-resizeValue.x, -resizeValue.y);
            }
            //Upper Right
            else if (rectTransform.pivot == new Vector2(0, 0))
            {
                rectTransform.sizeDelta += new Vector2(resizeValue.x, resizeValue.y);
            }
            //Upper Left
            else if (rectTransform.pivot == new Vector2(1, 0))
            {
                rectTransform.sizeDelta += new Vector2(-resizeValue.x, resizeValue.y);
            }
        }

        #endregion
        #region If drag by sides

        else if (currentSide != null)
        {
            //Left Side
            if (rectTransform.pivot == new Vector2(1, 0.5f))
            {
                rectTransform.sizeDelta += new Vector2(-resizeValue.x, 0);
            }
            //Right Side
            else if (rectTransform.pivot == new Vector2(0, 0.5f))
            {
                rectTransform.sizeDelta += new Vector2(resizeValue.x, 0);
            }
            //Upper Side
            else if (rectTransform.pivot == new Vector2(0.5f, 0))
            {
                rectTransform.sizeDelta += new Vector2(0, resizeValue.y);
            }
            //Bottom Side
            else if (rectTransform.pivot == new Vector2(0.5f, 1))
            {
                rectTransform.sizeDelta += new Vector2(0, -resizeValue.y);
            }
        }

        #endregion
    }

    private RectTransform GetNearestResizePoint()
    {
        RectTransform nearestPoint = null;
        float minDistance = Mathf.Infinity;
        foreach (RectTransform point in resizePointsList)
        {
            float distance = Vector2.Distance(point.position, mousePos);
            if (distance < minDistance)
            {
                nearestPoint = point;
                minDistance = distance;
            }
        }
        return nearestPoint;
    }

    private IEnumerator ChangePlaceSize()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 startMousePos);

        while (isDrag)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 mousePos);
            Vector2 resizeValue = mousePos - startMousePos;

            ResizeDependOnPivot(resizeValue);

            Flip();

            startMousePos = mousePos;

            yield return null;
        }

        SetPivot(new Vector2(0.5f, 0.5f));

        SetStrectAnchorPreset();
    }

    #endregion

    #region Events

    public void OnPointerDown(PointerEventData eventData)
    {
        resizePoints.SetActive(true);
        sides.SetActive(true);

        if (currentSide == null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out mousePos);
            currentResizePoint = GetNearestResizePoint();
        }

        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;

        resizePoints.SetActive(true);
        sides.SetActive(true);

        SetCenterAnchorPreset();

        SetPivotDependOnCurrentResizePoint();

        StartCoroutine(ChangePlaceSize());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        foreach (var side in sidesList)
        {
            side.ResetRayCast();
        }

        ResetPointsAndSides();

        currentInfo.TopPad = rectTransform.offsetMax.y;
        currentInfo.BottomPad = rectTransform.offsetMin.y;
        currentInfo.LeftPad = rectTransform.offsetMin.x;
        currentInfo.RightPad = rectTransform.offsetMax.x;

        placesController.UpdatePlace(currentInfo);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (resizePoints.activeInHierarchy)
        {
            resizePoints.SetActive(false);
        }
        if (sides.activeInHierarchy)
        {
            sides.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData) { }

    #endregion
}