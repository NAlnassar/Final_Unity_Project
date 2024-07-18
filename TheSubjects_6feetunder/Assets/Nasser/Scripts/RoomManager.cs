using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField createroom;
    [SerializeField] TMP_InputField joinroom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createroom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinroom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
