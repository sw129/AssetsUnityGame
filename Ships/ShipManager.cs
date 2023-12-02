using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private GameObject _shipToSpawn;
    [SerializeField] private string _shipName;
    [SerializeField] private string _weaponTag;
    //скрипты для передачи
    [SerializeField] private ShipMoveControl _shipControlForce;
    [SerializeField] private LifePoint _lifePoint;
    [SerializeField] private WeaponeManager _weaponeControl;
    [SerializeField] private ShieldManager _shieldManager;

    private GameObject _curentShip;
    private List<GameObject> _weaponList;

    void Start()
    {
        SpawnShip(_shipToSpawn);
    }

    private List<GameObject> GetChildByTag(string findTag)
    {
        List<GameObject> findObject = new();

        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.GameObject().CompareTag(findTag))
            {
                findObject.Add(g.GameObject());
            }
        }

        return findObject;
    }

    private void SpawnShip(GameObject ship)
    {
        _curentShip = Instantiate(ship, transform, false);
        _curentShip.name = _shipName;
        FindShipComponent();
        SetShipCharacteristics();
    }

    private void FindShipComponent()
    {
        _weaponList = GetChildByTag(_weaponTag);
    }

    public void SetShip(GameObject ship)
    {
        Destroy(_curentShip);
        SpawnShip(ship);
    }

    private void SetShipCharacteristics()
    {
        //установить параметры движения
        _shipControlForce.SetCharacteristics(_curentShip.GetComponent<ShipCharacteristics>());
        //установить параметры жизний и щитов
        _lifePoint.SetCharacteristics(_curentShip.GetComponent<ShipCharacteristics>());
        //установить параметры оружия
        _weaponeControl.SetWeaponList(_weaponList);
        //установить размер щита
        _shieldManager.SetShieldSize(_curentShip.GetComponent<BoxCollider>().size);
    }
}
