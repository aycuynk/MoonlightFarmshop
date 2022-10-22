using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 camOffset;
    void Start()
    {
        camOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        transform.position = target.position + camOffset;
    }
}
