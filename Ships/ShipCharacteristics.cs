using UnityEngine;

public class ShipCharacteristics : MonoBehaviour
{
    //�����
    public float life = 200;

    //����
    public float shield = 500; 
    public float cooldownShieldRecovery = 5f;
    public float countPointsShieldRecovery = 10f;

    public float speedMovement = 90f; //������� �������� ��������
    public float accelerationSpeedMovement = 170f; // �������� �������� �� ����� ���������

    public float maxSpeedMovement = 450f; // ������� ������������ �������� ��������
    public float maxAccelerationSpeedMovement = 950f; // ������������ �������� �������� �� ����� ���������

    public float speedRotate = 4f; //������� �������� ��������
    public float accelerationSpeedRotate = 1.5f; // �������� �������� �� ����� ���������

    public float maxSpeedRotate = 8f; // ������� ������������ �������� ��������
    public float maxAccelerationSpeedRotate = 3f; // ������������ �������� �������� �� ����� ��������

}
