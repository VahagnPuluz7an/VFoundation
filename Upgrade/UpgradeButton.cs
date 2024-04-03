using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrade
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text lvlText;
        
        public event Action Clicked;
        
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        public void SetLvlText(int lvl)
        {
            lvlText.SetText($"Lvl {lvl}");
        }

        private void Click()
        {
            Clicked?.Invoke();
        }
    }
}