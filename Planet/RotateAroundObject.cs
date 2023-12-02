using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    [SerializeField] float angleRotate = 0.4f;
    [SerializeField] GameObject aroundObject;

    void FixedUpdate()
    {
        transform.RotateAround(aroundObject.transform.position, Vector3.up, angleRotate * Time.deltaTime);
    }
}
