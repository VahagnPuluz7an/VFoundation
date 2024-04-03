using System;
using UnityEngine;

namespace Upgrade
{
    [RequireComponent(typeof(UpgradeButton))]
    public abstract class UpgradeBehaviour : MonoBehaviour
    {
        public static event Action UpgradedStaticEvent;
        public event Action Upgraded;
        public float FinishValue => value * PlayerPrefs.GetInt(Key);

        [SerializeField] protected float value;
        [SerializeField] private ParticleSystem particle;
        
        private string Key => GetType().Name;
        private UpgradeButton _button;

        private void Awake()
        {
            _button = GetComponent<UpgradeButton>();
        }

        private void Start()
        {
            _button.Clicked += Upgrade;
        }

        private void OnEnable()
        {
            if (_button != null) _button.SetLvlText(PlayerPrefs.GetInt(Key) + 1);
        }
        
        private void OnDestroy()
        {
            _button.Clicked -= Upgrade;
        }

        protected void Upgrade()
        {
            UpgradeLevel();
            UpgradedStaticEvent?.Invoke();
            Upgraded?.Invoke();
            if (particle != null) particle.Play();
        }
        
        protected void UpgradeLevel()
        {
            int lastLevel =  PlayerPrefs.GetInt(Key);
            int newLevel = lastLevel + 1;
            PlayerPrefs.SetInt(Key, newLevel);
        }
    }
}