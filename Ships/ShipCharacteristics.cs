using UnityEngine;

public class ShipCharacteristics : MonoBehaviour
{
    //Жизни
    public float life = 200;

    //Щиты
    public float shield = 500; 
    public float cooldownShieldRecovery = 5f;
    public float countPointsShieldRecovery = 10f;

    public float speedMovement = 90f; //Обычная скорость движения
    public float accelerationSpeedMovement = 170f; // Скорость движения во время ускорения

    public float maxSpeedMovement = 450f; // Обычная максимальная скорость движения
    public float maxAccelerationSpeedMovement = 950f; // Максимальная скорость движения во время ускорения

    public float speedRotate = 4f; //Обычная скорость поворота
    public float accelerationSpeedRotate = 1.5f; // Скорость поворота во время ускорения

    public float maxSpeedRotate = 8f; // Обычная максимальная скорость поворота
    public float maxAccelerationSpeedRotate = 3f; // Максимальная скорость поворота во время усорения

}
