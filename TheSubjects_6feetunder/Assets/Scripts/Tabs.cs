using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour
{
    public GameObject[] tabs;
    public Button previousButton;
    public Button nextButton;

    private int currentTabIndex = 0;

    void Start()
    {
        UpdateTabs();
        previousButton.onClick.AddListener(PreviousTab);
        nextButton.onClick.AddListener(NextTab);
    }

    void UpdateTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == currentTabIndex);
        }

        previousButton.interactable = currentTabIndex > 0;
        nextButton.interactable = currentTabIndex < tabs.Length - 1;
    }

    void PreviousTab()
    {
        if (currentTabIndex > 0)
        {
            currentTabIndex--;
            UpdateTabs();
        }
    }

    void NextTab()
    {
        if (currentTabIndex < tabs.Length - 1)
        {
            currentTabIndex++;
            UpdateTabs();
        }
    }
}

