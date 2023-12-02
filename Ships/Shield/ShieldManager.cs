using System.Collections;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] private GameObject _shieldToSpawn; // ������ ����
    [SerializeField] private string _shieldName = "Shield";
    [SerializeField] private KeyCode _shieldSwitchKey = KeyCode.Space; // �������, ������� ����� �������������� ��� ������������
    [SerializeField] private float _scaleSizeShield = 1.5f;
    [SerializeField] private Color _shiedColor = Color.blue;
    private Animator _animator;

    private Renderer _rend; // ��������� Renderer �������
    private GameObject _curentShield; // ��� �� �����

    private void Update()
    {
        // ���������, ������ �� �������
        if (Input.GetKeyDown(_shieldSwitchKey))
        {
            if (_animator.GetBool("ShieldState"))
            {
                //��������� ���
                _animator.SetTrigger("ShieldOff");
            }
            else
            {
                //�������� ���
                _animator.SetTrigger("ShieldOn");
            }
        }
    }

    public void SetShieldSize(Vector3 shieldSize)
    {
        //������ ��� � �������� ��� ����������
        SpawnShield(_shieldToSpawn);
        //������������� ������� ����
        _curentShield.transform.localScale = shieldSize * _scaleSizeShield;
    }

    private void SpawnShield(GameObject shield)
    {
        //�������� ������� ���
        _curentShield = Instantiate(shield, transform, false);
        //�������� ��������� ������
        _rend = _curentShield.GetComponent<Renderer>();
        //������������� ���� ���������
        _rend.material.color = _shiedColor;
        //������������� �������� ��� ������� ���
        _curentShield.name = _shieldName;
        //�������� ��������
        _animator = _curentShield.GetComponent<Animator>();
    }
}
