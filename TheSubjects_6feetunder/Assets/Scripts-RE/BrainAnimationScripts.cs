using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainAnimationScripts : MonoBehaviour
{


    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 360f) * 0.2f* Time.deltaTime);
    }
}
