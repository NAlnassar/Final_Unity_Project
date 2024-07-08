using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class player_generator : MonoBehaviour
{
    [SerializeField] GameObject[] player_prefab;
    static int num_players = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate(player_prefab[num_players].name, new Vector3(0,
                              2, 0), Quaternion.identity, 0);
        num_players++;
    }

    // Update is called once per frame
}
