using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_num : MonoBehaviour
{
    public int num_players = 0;
    public Vector3[] spawn_points = new Vector3[2];
    public int num_Presses = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawn_points[0] = new Vector3(-60, 0.2f, -23);
        spawn_points[1] = new Vector3(-60, 0.2f, -27);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
