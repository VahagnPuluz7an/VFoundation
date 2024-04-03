using UnityEngine;

namespace VFoundation.Helpers
{
    public class LookAtToObject : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool toCamera = true;

        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = References.MainCamera.transform;
        }

        private void Update()
        {
            transform.LookAt(toCamera ? _cameraTransform : target);
        }
    }
}