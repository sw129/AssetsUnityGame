using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    private RectTransform _transform;
    private RectTransform _parent;
    private Camera _camera;
    [SerializeField] private Transform _target;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _transform = transform as RectTransform;
        _parent = transform.parent as RectTransform;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(_target.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, screenPoint, null, out Vector2 point);
        _transform.anchoredPosition = point;
    }
}
