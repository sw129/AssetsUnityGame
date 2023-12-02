using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; // Префаб снаряда
    [SerializeField] private float _timeCooldown = 0.4f; // Длительность перезарядки
    [SerializeField] private Vector3 _offset = new(0, 0, 5f); // Смещение снаряда
    
    private const float _timeUpdatingCounter = 0.1f; // Время обновления счётчика в секундах
    private float _cooldown = 0f; // Оставшееся время до перезарядки 
    private GameObject _aim; // Цель к которой движется снаряд

    private void Start()
    {
        //Получаем цель
        _aim = GameObject.FindWithTag("Aim");
    }

    public void Shoot()
    {
        //выстрел
        if (_cooldown == 0)
        {
            SpawnBullet(); // Создаём снаряд
            StartCoroutine(Cooldown()); // Запускаем счётчик до перезарядки
        }
    }

    private void SpawnBullet()
    {
        Vector3 bulletPos = transform.TransformPoint(_offset); // кординаты создания снаряда
        Vector3 relativePos = _aim.transform.position - bulletPos; // направление от снаряда к целе
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, -Vector3.up); // поворот снаряда к целе

        Instantiate(_bulletPrefab, bulletPos, targetRotation); // создание снаряда
    }

    IEnumerator Cooldown() 
    {
        // устанавливаем время перезарядки
        _cooldown = _timeCooldown;
        // отчёт до перезарядки
        while (_cooldown > 0)
        {
            _cooldown -= _timeUpdatingCounter;
            yield return new WaitForSeconds(_timeUpdatingCounter);
        }
        _cooldown = 0;
    }

}
