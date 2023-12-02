using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private InputProvider _inputProvider;
    [SerializeField] private float _ZoomSpeed = 1.1f;
    [SerializeField] private float _ZoomOutSpeed = 1.1f;
    [SerializeField] private Vector3 _maxOffsetCamera = new (0, 8f, -24f);

    private Vector3 _satrtTransform;

    void Start()
    {
        _satrtTransform = transform.localPosition;
    }

    void Update()
    {
        if (_inputProvider.AccelerationSpeed)
        {
            //Отдаление камеры
            MoveCameraToPoint(_maxOffsetCamera, _ZoomSpeed);
        }
        else
        {
            //Приближение камеры
            MoveCameraToPoint(_satrtTransform, _ZoomOutSpeed);
        }
    }

    void MoveCameraToPoint(Vector3 point, float spead)
    {
        //Двигает камеру к точке
        transform.localPosition = Vector3.Lerp(transform.localPosition, point, spead);
    }
}
