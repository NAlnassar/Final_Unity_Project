using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opening_door : MonoBehaviour
{
    public Animator anim_1;
    public Animator anim_2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("It Worked");
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Debug.Log("It Worked again");
            anim_1.SetBool("open_door", true);
            anim_2.SetBool("open_door", true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("It Worked Thrice");
            StartCoroutine(door_close_timer());
        }
    }

    IEnumerator door_close_timer()
    {
        yield return new WaitForSeconds(8);
        anim_1.SetBool("open_door", false);
        anim_2.SetBool("open_door", false);
    }
}
