using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    public virtual float VerticalInput { get; }
    public virtual float HorizontalInput { get; }
    public virtual float InputMouseX { get; }
    public virtual float InputMouseY { get; }
    public virtual bool AccelerationSpeed { get; }

}
