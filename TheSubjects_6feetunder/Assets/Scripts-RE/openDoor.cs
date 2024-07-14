using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public Animator doorU;
    public Animator doorD;

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player2" || other.gameObject.tag == "Player1")
        {
            doorU.SetBool("open", true);
            Debug.Log("Touched trigger");
            doorD.SetBool("open", true);
        }
    }

 
}
