using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class FollowPlayer : MonoBehaviour
{
    // ������ �� ��������� rigidbody �������
    private Rigidbody _rb;

    [SerializeField] private string _TargetTag = "Player";

    // �������� �������� ������� � �������� � �������
    private float _rotateSpeed = 90f;

    // ����, �����������, ��� player ��������� � ������� ���� �������
    private bool _TargetInTrigger = false;

    private GameObject _target = null;
    private float _speed = 50f;

    // �����, ���������� ��� ������ �����
    private void Start()
    {
        _rb  =  GetComponent<Rigidbody>();
    }

    // �����, ���������� ��� ���������� �����
    private void FixedUpdate()
    {
        // ���� player ��������� � ������� ���� �������
        if (_TargetInTrigger)
        {
            // ��������� ����������� �� ������� � player
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            // ��������� ��� ��������, ������ ������� ����� ��������� ������
            Vector3 axis = Vector3.Cross(direction, Vector3.up);
            // ��������� ������ ���� � �������
            _rb.AddTorque(axis * _rotateSpeed);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 10);
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            float angle = Vector3.Angle(_target.transform.position, transform.position);

            // ��������� ������� ������� ������� � ������� player
            //Quaternion targetRotation = Quaternion.LookRotation(direction);

            //_rb.AddTorque(direction * _rotateSpeed);

            // ������ ������������ ������ � ������� ������� � �������� ���������
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

            if (distance > 30)
            {
                _rb.AddForce(transform.forward * _speed);
            }
            else
            {
                _rb.angularVelocity = Vector3.ClampMagnitude(_rb.angularVelocity, 90);
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 90);
            }

        }
    }

    // �����, ���������� ��� ����� ������� ���������� � ������� ���� �������
    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ��������� ����������� player
        if (other.gameObject.CompareTag(_TargetTag))
        {
            // ������������� ����, ��� player ��������� � ������� ���� �������
            _TargetInTrigger = true;
            _target = other.gameObject;
        }
    }

    // �����, ���������� ��� ������ ������� ���������� �� ������� ���� �������
    private void OnTriggerExit(Collider other)
    {
        // ���� ������ ��������� ����������� player
        if (other.gameObject.CompareTag(_TargetTag))
        {
            // ���������� ����, ��� player ��������� � ������� ���� �������
            _TargetInTrigger = false;
            _target = null;
        }
    }
}
