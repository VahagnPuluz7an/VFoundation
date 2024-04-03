using DSystem;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace VFoundation
{
    [DisableInitialize]
    public class AudioSettings : DBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private SwitchToggle audioToggle;
    
        private float _audioVolume = 1.0f;
        private float _musicVolume = 1.0f;

        protected override void OnInitialized()
        {
            _audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            
            audioToggle.Init();
            
            musicSlider.value = _musicVolume;
            audioToggle.Toggle.isOn = _audioVolume >= 0;
            
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            audioToggle.Toggle.onValueChanged.AddListener(AudioToggleSwitched);

            Observable.TimerFrame(2).Subscribe(x =>
            {
                audioMixer.SetFloat("AudioVolume", _audioVolume);
                audioMixer.SetFloat("MusicVolume", _musicVolume);
            }).AddTo(this);
        }

        protected override void OnDestroy()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            audioToggle.Toggle.onValueChanged.RemoveAllListeners();
        }

        private void AudioToggleSwitched(bool active)
        {
            if (active)
            {
                SetAudioVolume(0f);
                return;
            }

            SetAudioVolume(-80f);
        }

        private void SetAudioVolume(float volume)
        {
            _audioVolume = volume;
            audioMixer.SetFloat("AudioVolume", _audioVolume);
            PlayerPrefs.SetFloat("AudioVolume", _audioVolume);
        }

        private void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            audioMixer.SetFloat("MusicVolume", _musicVolume);
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        }
    }
}