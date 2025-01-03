using UnityEngine;

namespace Vahag_s_Assets.Utils
{
    public class Rotor : MonoBehaviour
    {
        [SerializeField] private Vector3 axis;
        [SerializeField] private float angle;
        [SerializeField] private Space space;

        private void Update()
        {
            transform.Rotate(axis, angle * Time.deltaTime, space);
        }
    }
}
