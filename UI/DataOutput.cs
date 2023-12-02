using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataOutput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _movementSpeedFiled;
    [SerializeField] private TextMeshProUGUI _rotateSpeedFiled;

    [SerializeField] private Image _lineLife;
    [SerializeField] private Image _lineShield;

    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private Image _weaponImage;

    private void Start()
    {
        OutputSpeedInformation(0, 0);

        ShipMoveControl.OnSpeedChanged += OutputSpeedInformation;
        LifePoint.OnShieldPointsUpdate += ChangLineShield;
        LifePoint.OnLifePointsUpdate += ChangLineLife;
        WeaponeManager.OnWeaponGroupChanged += ChangWeapon;
    }

    private void OutputSpeedInformation(float movementSpeed, float rotateSpeed)
    {
        //вывод информации
        _movementSpeedFiled.text = "Скорость движения:" + Math.Round(movementSpeed, 3);
        _rotateSpeedFiled.text = "Скорость поворота:" + Math.Round(rotateSpeed, 3);
    }

    private void ChangLineLife(float lifePoints, float maxLifePoints)
    {
        _lineLife.fillAmount = lifePoints / maxLifePoints;
    }

    private void ChangLineShield(float shieldPoints, float maxShieldPoints)
    {
        _lineShield.fillAmount = shieldPoints / maxShieldPoints;
    }

    private void ChangWeapon(Sprite weaponImage, string weaponName)
    {
        _weaponName.text = weaponName;
        _weaponImage.sprite = weaponImage;
    }
}
