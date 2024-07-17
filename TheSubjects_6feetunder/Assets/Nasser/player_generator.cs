using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player_generator : MonoBehaviour
{
    [SerializeField] GameObject[] player_prefab;
    check_num check;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<check_num>();
        int check_number_players = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        GameObject player = PhotonNetwork.Instantiate(player_prefab[check_number_players].name,
            check.spawn_points[check_number_players] , new Quaternion(0, 90,0, 90), 0);
    }

    // Update is called once per frame
}
