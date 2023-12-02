using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LifePoint : MonoBehaviour
{
    private float _maxLifePoint = 1f;
    private float _maxShieldPoint = 0f;
    private float _cooldownShieldRecovery = 0f;
    private float _countPointsShieldRecovery = 0f;
    private float _curentTimeCooldoownShieldRecovery = 0f;
    private float _curentLifePoint;
    private float _curentShieldPoint;

    // ќпредел€ем делегаты дл€ изменени€ UI щита
    public delegate void ShieldPointsUpdateUI(float shieldPoints, float maxShieldPoints);
    // ќпредел€ем событие, которое использует этот делегат
    public static event ShieldPointsUpdateUI OnShieldPointsUpdate;

    // ќпредел€ем делегаты дл€ изменени€ UI жизни
    public delegate void LifePointsUpdateUI(float lifeldPoints, float maxLifePoints);
    // ќпредел€ем событие, которое использует этот делегат
    public static event LifePointsUpdateUI OnLifePointsUpdate;

    void Start()
    {
        _curentLifePoint = _maxLifePoint;
        _curentShieldPoint = _maxShieldPoint;

    }

    void Update()
    {
        DecreaseShieldPoints(1f);
        RecoveryShieldPoints();
    }

    private void DecreaseLifePoints(float decreasePoints)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _curentLifePoint -= decreasePoints;
            OnLifePointsUpdate?.Invoke(_curentLifePoint, _maxShieldPoint);
        }
    }

    private void DecreaseShieldPoints(float decreasePoints)
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            _curentShieldPoint -= decreasePoints;
            OnShieldPointsUpdate?.Invoke(_curentShieldPoint, _maxShieldPoint);
            _cooldownShieldRecovery = 0;
        }
    }

    private void RecoveryShieldPoints()
    {
        _curentTimeCooldoownShieldRecovery += Time.deltaTime;
        if (_curentTimeCooldoownShieldRecovery > _cooldownShieldRecovery && _curentShieldPoint < _maxShieldPoint)
        {
            _curentShieldPoint += _countPointsShieldRecovery;
        }
    }

    public void SetCharacteristics(ShipCharacteristics shipCh)
    {
        _maxLifePoint = shipCh.life;
        _maxShieldPoint = shipCh.shield;
        _cooldownShieldRecovery = shipCh.cooldownShieldRecovery;
        _countPointsShieldRecovery = shipCh.countPointsShieldRecovery;
        _curentTimeCooldoownShieldRecovery = 0f;
        _curentLifePoint = _maxLifePoint;
        _curentShieldPoint = _maxShieldPoint;
    }
}
