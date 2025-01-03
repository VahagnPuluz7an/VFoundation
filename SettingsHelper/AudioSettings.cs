using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings
{
    public class AudioSettings : MonoBehaviour, IDisableObject
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider audioToggle;
    
        private float _audioVolume = 1.0f;
        private float _musicVolume = 1.0f;
        
        public void Initialize()
        {
            _audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            
            musicSlider.value = _musicVolume;
            audioToggle.value = _audioVolume;
            
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            audioToggle.onValueChanged.AddListener(SetAudioVolume);

            Observable.TimerFrame(2).Subscribe(x =>
            {
                audioMixer.SetFloat("AudioVolume", _audioVolume);
                audioMixer.SetFloat("MusicVolume", _musicVolume);
            }).AddTo(this);
        }

        public void Dispose()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            audioToggle.onValueChanged.RemoveAllListeners();
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