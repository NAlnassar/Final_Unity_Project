using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSpin : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 360f, 0) * 1 * Time.deltaTime);
    }
}
