using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    private PlaceInfo currentInfo;
    public void Set(PlaceInfo info)
    {
        currentInfo = info;
    }
}