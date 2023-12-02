using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AttractionPlanet : MonoBehaviour
{
    private Rigidbody rb = null;

    private float m1;
    private float m2;
    private const float G = 6.67f;  // задаем гравитационную постоянную

    [SerializeField] float massMult = math.pow(10, 25);
    private Transform objectTransform;

    private void Start()
    {
        m2 = GetComponentInParent<Rigidbody>().mass * massMult;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb != null) // проверяем, что он существует
        {
            float distance = (objectTransform.position - transform.position).magnitude;
            Vector3 direction = (objectTransform.position - transform.position).normalized; //направление притяжения

           //Vector3 attractionDir = Vector3.MoveTowards(objectTransform.forward, direction, 10f);
           //Vector3 rotateDir = Vector3.RotateTowards(objectTransform.forward, direction, 10f, 0.0F);

            float forceMagnitude = G * m1 * m2 / (distance * distance);
            Debug.Log(forceMagnitude);
            Vector3 force = direction * forceMagnitude; // сила притяжения
            rb.AddForce(-force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // проверяем, что он существует
        {
            
            rb = other.gameObject.GetComponentInParent<Rigidbody>();
            objectTransform = rb.transform;

            m1 = rb.mass; // получаем массу объекта
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // проверяем, что он существует
        {
            objectTransform = null;
            rb = null;
            m1 = 0;
        }
    }
}
