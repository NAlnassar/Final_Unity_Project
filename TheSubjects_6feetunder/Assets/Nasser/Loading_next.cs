using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Loading_next : MonoBehaviourPunCallbacks
{
    public int time;
    public int scene_num;
    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine(counting());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator counting()
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.LoadLevel(scene_num);
    }
}
