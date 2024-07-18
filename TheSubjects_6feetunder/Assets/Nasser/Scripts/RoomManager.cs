using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    //TMP_InputField createroom;
    //TMP_InputField joinroom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Hi");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Hi");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
