using UnityEngine;

namespace Tutorial
{
    public class TutorialPartFollower : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector3 offset;
    
        private void Update()
        {
            transform.position = target.position + offset;
        }
    }
}
