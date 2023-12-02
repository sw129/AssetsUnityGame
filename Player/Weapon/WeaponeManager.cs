using System.Collections.Generic;
using UnityEngine;

public class WeaponeManager : MonoBehaviour
{
    //������ ������
    private Dictionary<string, List<GameObject>> _weaponGroupList;

    // ������ �������� ���������� ����� �� �������
    private int _currentWeaponGroupIndex = 0;

    // ������ ���� ������
    private List<string> _weaponGroupName;

    //������ ����������� ������
    private List<Sprite> _weaponGroupSprite;

    //���� � ����� �� ���������
    [SerializeField] private string _pathsWeaponSprite = "WeaponSprite/";


    //������� ��� ������ ���������� � ������� ������ ������
    public delegate void WeaponGroupChangedEvent(Sprite weaponSprite, string weaponName);
    // ���������� �������, ������� ���������� ���� �������
    public static event WeaponGroupChangedEvent OnWeaponGroupChanged;

    private void Start()
    {
        _weaponGroupSprite = new();
    }

    private void Update()
    {
        //�� ������� ��� �������� ����� �������� ��� ������� ������
        if (Input.GetMouseButtonDown(0))
        {
            ShootGroup(_weaponGroupName[_currentWeaponGroupIndex]);
        }
    }

    private void FixedUpdate()
    {
        //������ ������ ������
        SelctWeaponGroup();
    }

    private void ShootGroup(string keyGroup)
    {
        //��� ������� ������� ������ �������� ����� �������
        foreach (GameObject weapon in _weaponGroupList[keyGroup])
        {
            weapon.GetComponent<Weapon>().Shoot();
        }
    }

    private void SelctWeaponGroup()
    {
        float mouseWheel = Input.mouseScrollDelta.y;

        // ��������� ����������� �������� ������ ����
        if (mouseWheel > 0) // �����
        {
            // ��������� ������ �� �������
            _currentWeaponGroupIndex++;

            // ��������� ������� ������
            if (_currentWeaponGroupIndex >= _weaponGroupName.Count)
            {
                // �������� ������
                _currentWeaponGroupIndex = 0;
            }

            OutputOnUI();
        }
        else if (mouseWheel < 0) // ����
        {
            // ��������� ������ �� �������
            _currentWeaponGroupIndex--;

            // ��������� ������� ������
            if (_currentWeaponGroupIndex < 0)
            {
                // ��������� ������� ��������� �������� ������
                _currentWeaponGroupIndex = _weaponGroupName.Count - 1;
            }

            OutputOnUI();
        }
    }

    public void SetWeaponList(List<GameObject> weaponList)
    {
        // ��������� ������
        _weaponGroupList = new();

        // ������� ������ ������
        foreach (GameObject weapon in weaponList)
        {
            //�������� �������� ������ ������
            string weaponGroup = weapon.GetComponent<WeaponType>().weaponGroup;

            if (_weaponGroupList.ContainsKey(weaponGroup))
            {
                //���� ��� ���� ����� ������ ������
                _weaponGroupList[weaponGroup].Add(weapon);
            }
            else
            {
                //���� ����� ������ ������ ���
                _weaponGroupList.Add(weaponGroup, new List<GameObject> { weapon });
            }
        }

        SetWeaponGroupName();
        SetWeaponGrupSprite();
        OutputOnUI();
    }

    private void SetWeaponGroupName()
    {
        // �������� �������� ����� ������
        _weaponGroupName = new List<string>(_weaponGroupList.Keys);
    }

    private void SetWeaponGrupSprite()
    {
        // ��������� ������ �� ������
        if (_weaponGroupList.Count > 0)
        {
            foreach (Sprite sprite in _weaponGroupSprite)
            {
                Resources.UnloadAsset(sprite);
            }
            _weaponGroupSprite.Clear();
        }

        //�������� ������� �� �������� ������ �� ����
        foreach (string name in _weaponGroupName)
        {
            _weaponGroupSprite.Add(Resources.Load<Sprite>(_pathsWeaponSprite + name));
        }
    }

    private void OutputOnUI()
    {
        OnWeaponGroupChanged?.Invoke(_weaponGroupSprite[_currentWeaponGroupIndex], _weaponGroupName[_currentWeaponGroupIndex]);
    }
}
