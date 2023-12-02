using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; // ������ �������
    [SerializeField] private float _timeCooldown = 0.4f; // ������������ �����������
    [SerializeField] private Vector3 _offset = new(0, 0, 5f); // �������� �������
    
    private const float _timeUpdatingCounter = 0.1f; // ����� ���������� �������� � ��������
    private float _cooldown = 0f; // ���������� ����� �� ����������� 
    private GameObject _aim; // ���� � ������� �������� ������

    private void Start()
    {
        //�������� ����
        _aim = GameObject.FindWithTag("Aim");
    }

    public void Shoot()
    {
        //�������
        if (_cooldown == 0)
        {
            SpawnBullet(); // ������ ������
            StartCoroutine(Cooldown()); // ��������� ������� �� �����������
        }
    }

    private void SpawnBullet()
    {
        Vector3 bulletPos = transform.TransformPoint(_offset); // ��������� �������� �������
        Vector3 relativePos = _aim.transform.position - bulletPos; // ����������� �� ������� � ����
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, -Vector3.up); // ������� ������� � ����

        Instantiate(_bulletPrefab, bulletPos, targetRotation); // �������� �������
    }

    IEnumerator Cooldown() 
    {
        // ������������� ����� �����������
        _cooldown = _timeCooldown;
        // ����� �� �����������
        while (_cooldown > 0)
        {
            _cooldown -= _timeUpdatingCounter;
            yield return new WaitForSeconds(_timeUpdatingCounter);
        }
        _cooldown = 0;
    }

}
