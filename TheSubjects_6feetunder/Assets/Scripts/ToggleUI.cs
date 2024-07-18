using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public GameObject toggleElement;
    public Button showButton;
    public Button hideButton;

    void Start()
    {
        showButton.onClick.AddListener(ShowElement);
        hideButton.onClick.AddListener(HideElement);
    }

    void ShowElement()
    {
        toggleElement.SetActive(true);
    }

    void HideElement()
    {
        toggleElement.SetActive(false);
    }
}
