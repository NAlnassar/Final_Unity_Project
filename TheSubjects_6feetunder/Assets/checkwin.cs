using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class checkwin : MonoBehaviourPunCallbacks
{
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 2)
        {
            PhotonNetwork.LoadLevel(7);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
    }
}
