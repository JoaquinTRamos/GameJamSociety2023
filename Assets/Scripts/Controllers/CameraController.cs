using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float smoothTime = 0.5f;

    [SerializeField] float maxSpeed = 15f;

    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
            targetOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + targetOffset,
                                                ref velocity, smoothTime, maxSpeed);
    }
}
