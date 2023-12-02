using UnityEngine;

public class ShipMoveControl : MonoBehaviour
{
    //��� ����
    [SerializeField] private ForceMode _forceMode = ForceMode.Force;
    [SerializeField] private InputProvider _inputProvider;

    private Rigidbody _rb;

    private float _speedMovement = 0f; //������� �������� ��������
    private float _accelerationSpeedMovement = 0f; // �������� �������� �� ����� ���������

    private float _maxAccelerationSpeedMovement = 0f; // ������������ �������� �������� �� ����� ���������
    private float _maxSpeedMovement = 0f; // ������������ �������� ��������

    private float _speedRotate = 0f; //������� �������� ��������
    private float _accelerationSpeedRotate = 0f; // �������� �������� �� ����� ���������

    private float _maxSpeedRotate = 0f; // ������������ �������� ��������
    private float _maxAccelerationSpeedRotate = 0f; // ������������ �������� �������� �� ����� ��������

    private bool _accelerationflag = false; // ���� ���������

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

    // ���������� �������, ������� ��������� ������������ ��������
    public delegate void SpeedChangedEvent(float movementSpeed, float rotateSpeed);

    // ���������� �������, ������� ���������� ���� �������
    public static event SpeedChangedEvent OnSpeedChanged;

    // ���� � ���������� � ����
    private float _verticalInput;
    private float _horizontalInput;
    private float _inputMouseX;
    private float _inputMouseY;

    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // �������� ��������� Rigidbody
    }

    void Update()
    {
        // �������� ���� �� ���������� ���������
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
        //�������� ����� ��� �����
        _rb.AddForce(_verticalInput * SpeedMovement * transform.forward, _forceMode);
    }

    private void Roll()
    {
        //��������
        _rb.AddTorque(_horizontalInput * SpeedRotate * transform.forward, _forceMode);
    }

    private void Rotate()
    {
        //������� ����� �� �����
        _rb.AddTorque(_inputMouseY * SpeedRotate * transform.right, _forceMode);
        _rb.AddTorque(_inputMouseX * SpeedRotate * transform.up, _forceMode);
    }

    private void LimitSpeed()
    {
        //����������� ��������
        _rb.angularVelocity = Vector3.ClampMagnitude(_rb.angularVelocity, MaxSpeedRotate);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MaxSpeedMovement);
    }

    private void StabilizeSpeed()
    {
        //��������� �������� �� 0
        _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, SpeedRotate * Time.deltaTime);
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, SpeedMovement * Time.deltaTime);
    }

    private void InformationOutput()
    {
        //�������� ���������� � ��������
        //OnSpeedChanged?.Invoke(_rb.velocity.magnitude, _rb.angularVelocity.magnitude);
        OnSpeedChanged?.Invoke(_rb.velocity.magnitude, Vector3.SignedAngle(_rb.angularVelocity, transform.forward, transform.up));

    }

    public void SetCharacteristics(ShipCharacteristics shipCh)
    {
        _speedMovement = shipCh.speedMovement; // ������� �������� ��������
        _accelerationSpeedMovement = shipCh.accelerationSpeedMovement; // �������� �������� �� ����� ���������

        _maxSpeedMovement = shipCh.maxSpeedMovement; // ������������ �������� ��������
        _maxAccelerationSpeedMovement = shipCh.maxAccelerationSpeedMovement; // ������������ �������� �������� �� ����� ���������

        _speedRotate = shipCh.speedRotate; // ������� �������� ��������
        _accelerationSpeedRotate = shipCh.accelerationSpeedRotate; // �������� �������� �� ����� ���������

        _maxSpeedRotate = shipCh.maxSpeedRotate; // ������������ �������� ��������
        _maxAccelerationSpeedRotate = shipCh.maxAccelerationSpeedRotate; // ������������ �������� �������� �� ����� ��������
    }
}
