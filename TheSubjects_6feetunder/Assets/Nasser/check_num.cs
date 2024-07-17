using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class check_num : MonoBehaviourPunCallbacks
{
    public int num_players = 0;
    public Vector3[] spawn_points = new Vector3[2];
    public int num_Presses = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(num_Presses  == 3)
        {
            PhotonNetwork.LoadLevel(3);
        }
    }
}
