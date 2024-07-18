using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;

    void Start()
    {
        // Ensure the UI panel is initially hidden
        uiPanel.SetActive(false);
    }

    public void ToggleUI()
    {
        // Toggle the visibility of the UI panel
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}