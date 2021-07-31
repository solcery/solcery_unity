using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceListObject : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private Toggle isVisibleToggle;
    [SerializeField]
    private Image background;
    [SerializeField]
    private TMP_InputField nameField;
    [SerializeField]
    private Image advice;
    [SerializeField]
    private TMP_Text adviceText;
    [SerializeField]
    private GameObject infoIcon;
    [SerializeField]
    private GameObject cancelIcon;

    [Header("Colors"), SerializeField]
    private Color correctColor;
    [SerializeField]
    private Color uncorrectColor;

    private PlaceInfo currentInfo;
    private PlacesController parent;

    private void Start()
    {
        parent = FindObjectOfType<PlacesController>();
        isVisibleToggle.onValueChanged.AddListener(OnIsVisibleChanged);
    }

    public void Set(PlaceInfo info)
    {
        currentInfo = info;

        isVisibleToggle.isOn = info.IsVisible;
        nameField.onValueChanged.AddListener(OnInputFieldChanged);
        nameField.text = string.Empty;
    }

    public void OnIsVisibleChanged(bool value)
    {
        currentInfo.IsVisible = isVisibleToggle.isOn;
        parent.UpdatePlace(currentInfo);
    }

    public void OnDeleteClicked()
    {
        parent.DeletePlace(currentInfo);
    }

    public void OnInputFieldChanged(string value)
    {
        if (string.IsNullOrEmpty(nameField.text))
        {
            background.color = uncorrectColor;
            advice.gameObject.SetActive(true);
            adviceText.text = "Enter the id of the place";
            cancelIcon.SetActive(false);
            infoIcon.SetActive(true);

            if (currentInfo != null && parent != null)
            {
                currentInfo.Id = string.Empty;
                parent.UpdatePlace(currentInfo);
            }
        }
        else
        {
            if (parent.IsIdFree(nameField.text))
            {
                background.color = correctColor;
                advice.gameObject.SetActive(false);

                currentInfo.Id = nameField.text;
                parent.UpdatePlace(currentInfo);
            }
            else
            {
                background.color = uncorrectColor;
                advice.gameObject.SetActive(true);
                adviceText.text = "Place id should not be repeated";
                cancelIcon.SetActive(false);
                infoIcon.SetActive(true);
            }
        }
    }
}