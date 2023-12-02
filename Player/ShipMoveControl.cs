using UnityEngine;

public class ShipMoveControl : MonoBehaviour
{
    //Тип силы
    [SerializeField] private ForceMode _forceMode = ForceMode.Force;
    [SerializeField] private InputProvider _inputProvider;

    private Rigidbody _rb;

    private float _speedMovement = 0f; //Обычная скорость движения
    private float _accelerationSpeedMovement = 0f; // Скорость движения во время ускорения

    private float _maxAccelerationSpeedMovement = 0f; // Максимальная скорость движения во время ускорения
    private float _maxSpeedMovement = 0f; // Максимальная скорость движения

    private float _speedRotate = 0f; //Обычная скорость поворота
    private float _accelerationSpeedRotate = 0f; // Скорость поворота во время ускорения

    private float _maxSpeedRotate = 0f; // Максимальная скорость поворота
    private float _maxAccelerationSpeedRotate = 0f; // Максимальная скорость поворота во время усорения

    private bool _accelerationflag = false; // Флаг Ускорения

    private float SpeedMovement
    {
        get
        {
            if (_accelerationflag)
            {
                return _accelerationSpeedMovement;
            }
            else
            {
                return _speedMovement;
            }
        }
    }

    private float MaxSpeedMovement
    {
        get
        {
            if (_accelerationflag)
            {
                return _maxAccelerationSpeedMovement;
            }
            else
            {
                return _maxSpeedMovement;
            }
        }
    }

    private float SpeedRotate
    {
        get
        {
            if (_accelerationflag)
            {
                return _accelerationSpeedRotate;
            }
            else
            {
                return _speedRotate;
            }
        }
    }

    private float MaxSpeedRotate
    {
        get
        {
            if (_accelerationflag)
            {
                return _maxAccelerationSpeedRotate;
            }
            else
            {
                return _maxSpeedRotate;
            }
        }
    }

    // Определяем делегат, который принимает вещественный параметр
    public delegate void SpeedChangedEvent(float movementSpeed, float rotateSpeed);

    // Определяем событие, которое использует этот делегат
    public static event SpeedChangedEvent OnSpeedChanged;

    // Ввод с клавиатуры и мыши
    private float _verticalInput;
    private float _horizontalInput;
    private float _inputMouseX;
    private float _inputMouseY;

    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
    }

    void Update()
    {
        // Получаем ввод от выбранного источника
        _verticalInput = _inputProvider.VerticalInput;
        _horizontalInput = _inputProvider.HorizontalInput;
        _inputMouseX = _inputProvider.InputMouseX;
        _inputMouseY = _inputProvider.InputMouseY;
        _accelerationflag = _inputProvider.AccelerationSpeed;
    }

    void FixedUpdate()
    {
        Movement();
        Roll();

        if (Input.GetKey(KeyCode.Z))
        {
            StabilizeSpeed();
        }

        Rotate();

        LimitSpeed();
        InformationOutput();

    }

    private void Movement()
    {
        //движение вперёд или назад
        _rb.AddForce(_verticalInput * SpeedMovement * transform.forward, _forceMode);
    }

    private void Roll()
    {
        //Кручение
        _rb.AddTorque(_horizontalInput * SpeedRotate * transform.forward, _forceMode);
    }

    private void Rotate()
    {
        //поворот вслед за мышью
        _rb.AddTorque(_inputMouseY * SpeedRotate * transform.right, _forceMode);
        _rb.AddTorque(_inputMouseX * SpeedRotate * transform.up, _forceMode);
    }

    private void LimitSpeed()
    {
        //ограничение скорости
        _rb.angularVelocity = Vector3.ClampMagnitude(_rb.angularVelocity, MaxSpeedRotate);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MaxSpeedMovement);
    }

    private void StabilizeSpeed()
    {
        //Уменьшаем скорость до 0
        _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, SpeedRotate * Time.deltaTime);
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, SpeedMovement * Time.deltaTime);
    }

    private void InformationOutput()
    {
        //Передача информации о скорости
        //OnSpeedChanged?.Invoke(_rb.velocity.magnitude, _rb.angularVelocity.magnitude);
        OnSpeedChanged?.Invoke(_rb.velocity.magnitude, Vector3.SignedAngle(_rb.angularVelocity, transform.forward, transform.up));

    }

    public void SetCharacteristics(ShipCharacteristics shipCh)
    {
        _speedMovement = shipCh.speedMovement; // Обычная скорость движения
        _accelerationSpeedMovement = shipCh.accelerationSpeedMovement; // Скорость движения во время ускорения

        _maxSpeedMovement = shipCh.maxSpeedMovement; // Максимальная скорость движения
        _maxAccelerationSpeedMovement = shipCh.maxAccelerationSpeedMovement; // Максимальная скорость движения во время ускорения

        _speedRotate = shipCh.speedRotate; // Обычная скорость поворота
        _accelerationSpeedRotate = shipCh.accelerationSpeedRotate; // Скорость поворота во время ускорения

        _maxSpeedRotate = shipCh.maxSpeedRotate; // Максимальная скорость поворота
        _maxAccelerationSpeedRotate = shipCh.maxAccelerationSpeedRotate; // Максимальная скорость поворота во время усорения
    }
}
