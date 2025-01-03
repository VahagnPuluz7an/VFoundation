using UnityEngine;

namespace Utils
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        private void OnValidate()
        {
            if (target == null)
                return;
            
            transform.position = target.position + offset;
        }

        private void Update()
        {
            transform.position = target.position + offset;
        }
    }
}