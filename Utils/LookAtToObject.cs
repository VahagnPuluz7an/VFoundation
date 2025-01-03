using UnityEngine;

public class LookAtToObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool toCamera = true;
    [SerializeField] private Vector3 axis;
    [SerializeField] private Vector3 multiply = Vector3.one;
    
    private Transform _cameraTransform;

    private void OnEnable()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        var quaternion = Quaternion.LookRotation(toCamera ? _cameraTransform.position : target.position, axis);
        quaternion.x *= multiply.x;
        quaternion.y *= multiply.y;
        quaternion.z *= multiply.z;
        transform.rotation = quaternion;
    }
}