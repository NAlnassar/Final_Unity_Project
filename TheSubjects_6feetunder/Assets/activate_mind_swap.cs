using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class activate_mind_swap : MonoBehaviourPunCallbacks
{
    bool check = false;
    // Update is called once per frame
    void Update()
    {
        if (!check && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (GameObject.FindWithTag("Player1") != null && GameObject.FindWithTag("Player2") != null)
            {
                GameObject.FindWithTag("Player1").GetComponent<move>().ability = 2;

                GameObject.FindWithTag("Player2").GetComponent<move>().ability = 2;
                check = true;
            }
        }
    }
}
