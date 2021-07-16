using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacesController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Prefabs"), SerializeField]
    private PlaceObject placePrefab;
    [SerializeField]
    private PlaceListObject placeListObjectPrefab;

    [Header("Components"), SerializeField]
    private Transform listContent;
    [SerializeField]
    private RectTransform placesHolder;

    private bool isPlaceCreating = false;
    private List<PlaceInfo> createdPlaces = new List<PlaceInfo>();

    private void Start()
    {
        foreach (var placeListObject in listContent.GetComponentsInChildren<PlaceListObject>())
        {
            Destroy(placeListObject.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(placesHolder, Input.mousePosition, Camera.main, out Vector2 mousePoint) && !isPlaceCreating)
        {
            isPlaceCreating = true;

            CreateNewPlaceInfo(mousePoint);

            StartCoroutine(ChangePlaceSize());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPlaceCreating = false;

        createdPlaces.Last().Width = createdPlaces.Last().Object.GetComponent<RectTransform>().sizeDelta.x;
        createdPlaces.Last().Height = createdPlaces.Last().Object.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void DeletePlace(PlaceInfo infoToDelete)
    {
        var info = createdPlaces.Find(x => x == infoToDelete);

        if (info != null)
        {
            Destroy(info.Object.gameObject);
            Destroy(info.ListObject.gameObject);

            createdPlaces.Remove(info);
        }
    }

    public void UpdatePlace(PlaceInfo infoToUpdate)
    {
        var info = createdPlaces.Find(x => x.Id == infoToUpdate.Id);

        if (info != null)
        {
            var infoId = createdPlaces.IndexOf(info);
            createdPlaces[infoId] = infoToUpdate;

            createdPlaces[infoId].Object.gameObject.SetActive(info.IsVisible);
        }
    }

    private void CreateNewPlaceInfo(Vector2 mousePoint)
    {
        var newId = Mathf.Max(createdPlaces.Select(x => x.Id).ToArray()) + 1;
        var newPlaceInfo = new PlaceInfo()
        {
            Id = newId,
            Name = $"Place_{newId}",
            IsVisible = true
        };

        var newPlace = Instantiate(placePrefab, placesHolder);
        newPlace.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        newPlaceInfo.Object = newPlace;

        var newPlaceListObject = Instantiate(placeListObjectPrefab, listContent);
        newPlaceInfo.ListObject = newPlaceListObject;

        newPlace.Set(newPlaceInfo);
        newPlaceListObject.Set(newPlaceInfo);

        createdPlaces.Add(newPlaceInfo);

        newPlace.GetComponent<RectTransform>().localPosition = new Vector3(mousePoint.x, mousePoint.y, 0);
    }

    private IEnumerator ChangePlaceSize()
    {
        PlaceInfo currentPlace = createdPlaces.Last();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(currentPlace.Object.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 startMousePos);

        while (isPlaceCreating)
        {
            Vector2 sizeDelta = currentPlace.Object.GetComponent<RectTransform>().sizeDelta;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(currentPlace.Object.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 mousePos);
            Vector2 resizeValue = mousePos - startMousePos;

            sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
            sizeDelta = new Vector2(Mathf.Clamp(sizeDelta.x, 0, sizeDelta.x), Mathf.Clamp(sizeDelta.y, 0, sizeDelta.y));

            currentPlace.Object.GetComponent<RectTransform>().sizeDelta = sizeDelta;

            startMousePos = mousePos;

            yield return null;
        }
    }
}