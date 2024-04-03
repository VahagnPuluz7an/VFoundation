using UnityEngine;

namespace VFoundation.Helpers
{
    public static class References
    {
        private static Camera _mainCamera;
        
        public static Camera MainCamera
        {
            get
            {
                if (_mainCamera != null) return _mainCamera;
                
                _mainCamera = Camera.main;
                return _mainCamera;
            }
        }
    }
}
