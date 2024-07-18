using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disfunction : MonoBehaviour
{
    public GameObject Tab1;
    public GameObject Tab2;
    public GameObject Tab3;

    public Button Button1;
    public Button Button2;
    public Button Button3;

    void Start()
    {
        // Initialize all tabs as inactive
        Tab1.SetActive(false);
        Tab2.SetActive(false);
        Tab3.SetActive(false);

        // Add listeners to buttons
        Button1.onClick.AddListener(() => ShowTab(Tab1, new GameObject[] { Tab2, Tab3 }));
        Button2.onClick.AddListener(() => ShowTab(Tab2, new GameObject[] { Tab1, Tab3 }));
        Button3.onClick.AddListener(() => ShowTab(Tab3, new GameObject[] { Tab1, Tab2 }));
    }

    void ShowTab(GameObject tabToShow, GameObject[] tabsToHide)
    {
        // Set the active tab
        tabToShow.SetActive(true);

        // Disable the other tabs
        foreach (var tab in tabsToHide)
        {
            tab.SetActive(false);
        }
    }
}
