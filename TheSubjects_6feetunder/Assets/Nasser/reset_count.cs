using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_count : MonoBehaviour
{
    public check_num check_Num;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            check_Num.num_Presses = 0;
        }
    }
}
