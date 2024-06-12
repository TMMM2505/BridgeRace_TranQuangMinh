using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        }
        else{
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
