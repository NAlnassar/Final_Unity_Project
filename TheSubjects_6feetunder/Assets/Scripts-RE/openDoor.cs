using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public Animator doorU;
    public Animator doorD;

    private bool player1InTrigger = false;
    private bool player2InTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            player1InTrigger = true;
        }
        else if (other.gameObject.tag == "Player2")
        {
            player2InTrigger = true;
        }

        if (player1InTrigger && player2InTrigger)
        {
            doorU.SetBool("open", true);
            doorD.SetBool("open", true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            player1InTrigger = false;
        }
        else if (other.gameObject.tag == "Player2")
        {
            player2InTrigger = false;
        }

        if (!player1InTrigger || !player2InTrigger)
        {
            doorU.SetBool("open", false);
            doorD.SetBool("open", false);
      
        }
    }
}
