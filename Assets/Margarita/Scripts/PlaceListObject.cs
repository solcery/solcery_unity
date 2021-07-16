using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceListObject : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private TMP_Text objectName;
    [SerializeField]
    private Toggle isVisibleToggle;

    private PlaceInfo currentInfo;
    private PlacesController parent;

    private void Start()
    {
        parent = FindObjectOfType<PlacesController>();
    }

    public void Set(PlaceInfo info)
    {
        currentInfo = info;

        objectName.text = info.Name;
        isVisibleToggle.isOn = info.IsVisible;
    }

    public void OnIsVisibleChanged()
    {
        currentInfo.IsVisible = isVisibleToggle.isOn;
        parent.UpdatePlace(currentInfo);
    }

    public void OnDeleteClicked()
    {
        parent.DeletePlace(currentInfo);
    }
}