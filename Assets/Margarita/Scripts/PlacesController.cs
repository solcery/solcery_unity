using System.Collections.Generic;
using UnityEngine;

public class PlacesController : MonoBehaviour
{
    [Header("Prefabs"), SerializeField]
    private PlaceObject placePrefab;
    [SerializeField]
    private PlaceListObject placeListObjectPrefab;

    [Header("Components"), SerializeField]
    private Transform listContent;
    [SerializeField]
    private RectTransform placesHolder;

    private List<PlaceInfo> createdPlaces = new List<PlaceInfo>();

    private void Start()
    {
        foreach (var placeListObject in listContent.GetComponentsInChildren<PlaceListObject>())
        {
            Destroy(placeListObject.gameObject);
        }
    }

    public void OnAddNewPlaceClicked()
    {
        var newPlaceInfo = new PlaceInfo()
        {
            IsVisible = true
        };

        var newPlace = Instantiate(placePrefab, placesHolder);
        newPlaceInfo.Object = newPlace;

        var newPlaceListObject = Instantiate(placeListObjectPrefab, listContent);
        newPlaceInfo.ListObject = newPlaceListObject;

        newPlace.Set(newPlaceInfo);
        newPlaceListObject.Set(newPlaceInfo);

        createdPlaces.Add(newPlaceInfo);
    }

    public bool IsIdFree(string idToCheck)
    {
        return createdPlaces.Find(x => x.Id == idToCheck) == null;
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
}