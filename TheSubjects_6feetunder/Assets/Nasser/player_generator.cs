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
        if (PlayerPrefs.GetInt("character") == 0)
        {
            PlayerPrefs.SetInt("character", PhotonNetwork.CurrentRoom.PlayerCount);
        }
        
            GameObject player = PhotonNetwork.Instantiate(player_prefab[PlayerPrefs.GetInt("character") - 1].name,
                check.spawn_points[PlayerPrefs.GetInt("character") - 1], new Quaternion(0, 90, 0, 90), 0);
    }

    // Update is called once per frame
}
