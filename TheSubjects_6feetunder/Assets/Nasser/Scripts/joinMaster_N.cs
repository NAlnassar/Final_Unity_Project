using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class joinMaster_N : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene(1);
    }
}
