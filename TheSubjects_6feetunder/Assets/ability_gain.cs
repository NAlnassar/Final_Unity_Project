using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability_gain : MonoBehaviour
{
    public int abi = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out move m))
        {
            Debug.Log("We are here");
            m.ability = abi;
            Destroy(gameObject);         
        }
    }
}
