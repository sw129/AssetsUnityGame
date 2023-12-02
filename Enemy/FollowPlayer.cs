using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class FollowPlayer : MonoBehaviour
{
    // Ссылка на компонент rigidbody объекта
    private Rigidbody _rb;

    [SerializeField] private string _TargetTag = "Player";

    // Скорость поворота объекта в градусах в секунду
    private float _rotateSpeed = 90f;

    // Флаг, указывающий, что player находится в триггер зоне объекта
    private bool _TargetInTrigger = false;

    private GameObject _target = null;
    private float _speed = 50f;

    // Метод, вызываемый при старте сцены
    private void Start()
    {
        _rb  =  GetComponent<Rigidbody>();
    }

    // Метод, вызываемый при обновлении кадра
    private void FixedUpdate()
    {
        // Если player находится в триггер зоне объекта
        if (_TargetInTrigger)
        {
            // Вычисляем направление от объекта к player
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            // Вычисляем ось вращения, вокруг которой нужно повернуть объект
            Vector3 axis = Vector3.Cross(direction, Vector3.up);
            // Применяем момент силы к объекту
            _rb.AddTorque(axis * _rotateSpeed);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 10);
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            float angle = Vector3.Angle(_target.transform.position, transform.position);

            // Вычисляем целевую ротацию объекта в сторону player
            //Quaternion targetRotation = Quaternion.LookRotation(direction);

            //_rb.AddTorque(direction * _rotateSpeed);

            // Плавно поворачиваем объект к целевой ротации с заданной скоростью
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

            if (distance > 30)
            {
                _rb.AddForce(transform.forward * _speed);
            }
            else
            {
                _rb.angularVelocity = Vector3.ClampMagnitude(_rb.angularVelocity, 90);
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 90);
            }

        }
    }

    // Метод, вызываемый при входе другого коллайдера в триггер зону объекта
    private void OnTriggerEnter(Collider other)
    {
        // Если другой коллайдер принадлежит player
        if (other.gameObject.CompareTag(_TargetTag))
        {
            // Устанавливаем флаг, что player находится в триггер зоне объекта
            _TargetInTrigger = true;
            _target = other.gameObject;
        }
    }

    // Метод, вызываемый при выходе другого коллайдера из триггер зоны объекта
    private void OnTriggerExit(Collider other)
    {
        // Если другой коллайдер принадлежит player
        if (other.gameObject.CompareTag(_TargetTag))
        {
            // Сбрасываем флаг, что player находится в триггер зоне объекта
            _TargetInTrigger = false;
            _target = null;
        }
    }
}
