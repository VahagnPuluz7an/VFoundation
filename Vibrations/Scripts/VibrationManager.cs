using Lofelt.NiceVibrations;
using UnityEngine;

namespace Vibrations.Scripts
{
    public class VibrationManager : MonoBehaviour, IDisableObject
    {
        [SerializeField] private SwitchToggle switchToggle;
    
        private static bool _vibrationOn = true;

        public void Initialize()
        { 
            switchToggle.Init();
            switchToggle.Toggle.onValueChanged.AddListener(VibrationSetActive);
            switchToggle.Toggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
            _vibrationOn = switchToggle.Toggle.isOn;
        }

        public void Dispose()
        {
            switchToggle.Toggle.onValueChanged.RemoveAllListeners();
        }
    
        public static void VibrateVeryLight()
        {
            if (_vibrationOn)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.VeryLight);
        }
    
        public static void VibrateLight()
        {
            if (_vibrationOn)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        private static void VibrationSetActive(bool active)
        {
            _vibrationOn = active;

            PlayerPrefs.SetInt("Vibration", _vibrationOn ? 1 : 0);
        }
    }
}