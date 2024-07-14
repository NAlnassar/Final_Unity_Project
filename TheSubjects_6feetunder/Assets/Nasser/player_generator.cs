using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class player_generator : MonoBehaviour
{
    [SerializeField] GameObject[] player_prefab;
    check_num check;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<check_num>();
        GameObject player = PhotonNetwork.Instantiate(player_prefab[check.num_players].name, check.spawn_points[check.num_players] , new Quaternion(0, 90,0, 90), 0);
        check.num_players++;
    }

    // Update is called once per frame
}
