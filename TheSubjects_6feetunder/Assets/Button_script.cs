using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reconnecting()
    {
        SceneManager.LoadScene(0);
    }

    public void quitting()
    {
        Application.Quit();
    }
}
