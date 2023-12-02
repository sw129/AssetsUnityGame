using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float timeLife = 5f;
    [SerializeField] float damage = 50f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * bulletSpeed); // ������ �������� ������ ������� � ����������� ��� ��������� ��� Z

        Destroy(this.gameObject, timeLife); // ���������� ������
    }
}
