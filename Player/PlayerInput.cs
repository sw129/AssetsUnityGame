using UnityEngine;

public class PlayerInput : InputProvider
{
    [SerializeField] private bool _inversion = true;
    [SerializeField] private KeyCode _accelerationSpeedKey = KeyCode.LeftShift;

    public override float VerticalInput
    {
        get
        {
            if (Pause.gamePauseState) return 0f;

            return Input.GetAxis("Vertical");
        }
    }

    public override float HorizontalInput
    {
        get
        {
            if (Pause.gamePauseState) return 0f;

            return Input.GetAxis("Horizontal");
        }
    }

    public override float InputMouseX
    {
        get
        {
            if (Pause.gamePauseState) return 0f;

            return Input.GetAxis("Mouse X");
        }
    }

    public override float InputMouseY
    {
        get
        {
            if (Pause.gamePauseState) return 0f;

            return _inversion ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y");
        }
    }

    public override bool AccelerationSpeed => Input.GetKey(_accelerationSpeedKey);
}
