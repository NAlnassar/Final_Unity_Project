using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count_elec : MonoBehaviour
{
    bool pressed = false;
    public check_num check_Num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(check_Num.num_Presses == 0)
        {
            pressed = false;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed)
        {
            check_Num.num_Presses++;
            pressed = true;
        }
    }
}
