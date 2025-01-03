using System;
using UnityEngine;

namespace Utils
{
    public class OnEnableChecker : MonoBehaviour
    {
        public event Action Enabled;
        public event Action Disabled;
        
        private void OnEnable()
        {
            Enabled?.Invoke();
        }
        
        private void OnDisable()
        {
            Disabled?.Invoke();
        }
    }
}