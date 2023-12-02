using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private InputProvider _inputProvider;
    [SerializeField] private float _speed = 90f;
    [SerializeField] private float _radius = 80f;
    private float _localPosZ;
    private Vector2 _center;

    private void Start()
    {
        _localPosZ = transform.localPosition.z;
        _center = Vector2.zero;
    }

    void Update()
    {
        float inputMouseX = _inputProvider.InputMouseX;
        float inputMouseY = _inputProvider.InputMouseY;

        // ��������� ��������
        Vector2 offset = _speed * Time.deltaTime * new Vector3(inputMouseX, -inputMouseY);

        // ��������� ����� ������� �������
        Vector2 newLocalPosition = (Vector2)transform.localPosition + offset;

        // ���������, �� ������� �� ����� ������� �� ������� �����
        float r = Vector2.Distance(newLocalPosition, _center);

        if (r > _radius)
        {
            // ���� �������, �� ���� ��������� ����� � �������� ����� �� �������
            newLocalPosition = _radius * newLocalPosition.normalized;
        }

        transform.localPosition = new Vector3(newLocalPosition.x, newLocalPosition.y, _localPosZ);
    }
}
