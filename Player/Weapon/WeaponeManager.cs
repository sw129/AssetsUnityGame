using System.Collections.Generic;
using UnityEngine;

public class WeaponeManager : MonoBehaviour
{
    //Группы оружия
    private Dictionary<string, List<GameObject>> _weaponGroupList;

    // Индекс текущего выбранного ключа из словаря
    private int _currentWeaponGroupIndex = 0;

    // Список груп оружия
    private List<string> _weaponGroupName;

    //Список изображений оружия
    private List<Sprite> _weaponGroupSprite;

    //Путь к папке со спрайтами
    [SerializeField] private string _pathsWeaponSprite = "WeaponSprite/";


    //Делегат для вывода информации о текущей группе оружия
    public delegate void WeaponGroupChangedEvent(Sprite weaponSprite, string weaponName);
    // Определяем событие, которое использует этот делегат
    public static event WeaponGroupChangedEvent OnWeaponGroupChanged;

    private void Start()
    {
        _weaponGroupSprite = new();
    }

    private void Update()
    {
        //по нажатию ЛКМ вызываем метод выстрела для текущей группы
        if (Input.GetMouseButtonDown(0))
        {
            ShootGroup(_weaponGroupName[_currentWeaponGroupIndex]);
        }
    }

    private void FixedUpdate()
    {
        //Выбоор группы оружия
        SelctWeaponGroup();
    }

    private void ShootGroup(string keyGroup)
    {
        //Для каждого объекта группы вызываем метод выстрел
        foreach (GameObject weapon in _weaponGroupList[keyGroup])
        {
            weapon.GetComponent<Weapon>().Shoot();
        }
    }

    private void SelctWeaponGroup()
    {
        float mouseWheel = Input.mouseScrollDelta.y;

        // Проверить направление вращения колеса мыши
        if (mouseWheel > 0) // Вверх
        {
            // Увеличить индекс на единицу
            _currentWeaponGroupIndex++;

            // Проверить границы списка
            if (_currentWeaponGroupIndex >= _weaponGroupName.Count)
            {
                // Обнулить индекс
                _currentWeaponGroupIndex = 0;
            }

            OutputOnUI();
        }
        else if (mouseWheel < 0) // Вниз
        {
            // Уменьшить индекс на единицу
            _currentWeaponGroupIndex--;

            // Проверить границы списка
            if (_currentWeaponGroupIndex < 0)
            {
                // Присвоить индексу последнее значение списка
                _currentWeaponGroupIndex = _weaponGroupName.Count - 1;
            }

            OutputOnUI();
        }
    }

    public void SetWeaponList(List<GameObject> weaponList)
    {
        // Обновляем список
        _weaponGroupList = new();

        // Создаем группы оружия
        foreach (GameObject weapon in weaponList)
        {
            //Получаем название группы оружия
            string weaponGroup = weapon.GetComponent<WeaponType>().weaponGroup;

            if (_weaponGroupList.ContainsKey(weaponGroup))
            {
                //Если уже есть такая группа оружия
                _weaponGroupList[weaponGroup].Add(weapon);
            }
            else
            {
                //Если такой группы оружия нет
                _weaponGroupList.Add(weaponGroup, new List<GameObject> { weapon });
            }
        }

        SetWeaponGroupName();
        SetWeaponGrupSprite();
        OutputOnUI();
    }

    private void SetWeaponGroupName()
    {
        // Получаем названий групп оружия
        _weaponGroupName = new List<string>(_weaponGroupList.Keys);
    }

    private void SetWeaponGrupSprite()
    {
        // выгружаем спрайт из памяти
        if (_weaponGroupList.Count > 0)
        {
            foreach (Sprite sprite in _weaponGroupSprite)
            {
                Resources.UnloadAsset(sprite);
            }
            _weaponGroupSprite.Clear();
        }

        //Получаем спрайты по названию каждой из груп
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
