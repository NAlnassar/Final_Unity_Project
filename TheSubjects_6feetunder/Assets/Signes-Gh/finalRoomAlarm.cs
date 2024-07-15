using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalRoomAlarm : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(new Vector3(0f, 360f, 0) * 1 * Time.deltaTime);
    }
}
