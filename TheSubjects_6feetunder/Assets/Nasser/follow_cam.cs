using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_cam : MonoBehaviour
{
    Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            transform.position = follow.position;
            transform.rotation = follow.rotation;
        }  
    }

    public void SetFollowTarget(Transform target)
    {
        follow = target;
    }
}
