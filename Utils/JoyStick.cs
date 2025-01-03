using UnityEngine;

namespace Utility
{
    public class JoyStick : MonoBehaviour
    {
        public static bool Enabled = true;
        public static bool Mouse;

        private static Transform _Handle;

        public static Vector2 Move => _Handle.transform.localPosition / (Vector2.one * 100f);
        
        [SerializeField] private Transform handle;

        private Vector3 _start;

        private void Start()
        {
            _Handle = handle;
        }

        private void Update()
        {
            if (!Enabled)
            {
                transform.position = Vector3.up * 99999f;
                handle.localPosition = Vector3.zero;
                return;
            }

            Mouse = Input.GetMouseButton(0);
            
            if (Input.GetMouseButtonDown(0))
            {
                _start = Input.mousePosition;
                transform.position = _start;
            } 
            else if (Mouse)
            {
                Vector3 napr = Input.mousePosition - _start;
                napr = Vector3.ClampMagnitude(napr, 100f);
                handle.localPosition = napr;
            }
            else
            {
                transform.position = Vector3.up * 99999f;
                handle.localPosition = Vector3.zero;
            }
            
            #if UNITY_EDITOR

            float vert = Input.GetAxis("Vertical");
            float horiz = Input.GetAxis("Horizontal");
            
            if (vert != 0 || horiz != 0)
            {
                handle.localPosition = new Vector3(horiz, vert) * 100f;
            }
            
            #endif
        }
    }
}