using Lofelt.NiceVibrations;
using UnityEngine;

namespace VFoundation.Vibrations
{
    public class VibrationManager : MonoBehaviour
    {
        [SerializeField] private SwitchToggle switchToggle;

        private static bool _vibrationOn = true;

        private void Awake()
        {
            switchToggle.Init();
            switchToggle.Toggle.onValueChanged.AddListener(VibrationSetActive);
        }

        private void Start() => switchToggle.Toggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;

        public static void Vibrate()
        {
            if (_vibrationOn)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.VeryLight);
        }

        public void VibrationSetActive(bool active)
        {
            _vibrationOn = active;

            PlayerPrefs.SetInt("Vibration", _vibrationOn ? 1 : 0);
        }
    }
}