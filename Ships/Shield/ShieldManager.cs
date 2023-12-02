using System.Collections;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] private GameObject _shieldToSpawn; // Префаб щита
    [SerializeField] private string _shieldName = "Shield";
    [SerializeField] private KeyCode _shieldSwitchKey = KeyCode.Space; // Клавиша, которая будет использоваться для переключения
    [SerializeField] private float _scaleSizeShield = 1.5f;
    [SerializeField] private Color _shiedColor = Color.blue;
    private Animator _animator;

    private Renderer _rend; // Компонент Renderer объекта
    private GameObject _curentShield; // Щит на сцене

    private void Update()
    {
        // Проверяем, нажата ли клавиша
        if (Input.GetKeyDown(_shieldSwitchKey))
        {
            if (_animator.GetBool("ShieldState"))
            {
                //отключаем щит
                _animator.SetTrigger("ShieldOff");
            }
            else
            {
                //включаем щит
                _animator.SetTrigger("ShieldOn");
            }
        }
    }

    public void SetShieldSize(Vector3 shieldSize)
    {
        //создаём щит и получаем его компоненты
        SpawnShield(_shieldToSpawn);
        //Устанавливаем размеры щита
        _curentShield.transform.localScale = shieldSize * _scaleSizeShield;
    }

    private void SpawnShield(GameObject shield)
    {
        //Создание объекта щит
        _curentShield = Instantiate(shield, transform, false);
        //получаем компонент рендер
        _rend = _curentShield.GetComponent<Renderer>();
        //устанавливаем цвет материала
        _rend.material.color = _shiedColor;
        //устанавливаем название для объекта щит
        _curentShield.name = _shieldName;
        //получаем аниматор
        _animator = _curentShield.GetComponent<Animator>();
    }
}
