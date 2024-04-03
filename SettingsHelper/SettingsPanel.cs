using DSystem;
using UnityEngine;
using VFoundation.UI;

namespace VFoundation
{
    [DisableInitialize]
    [RequireComponent(typeof(AnimPanelActivity))]
    public class SettingsPanel : DBehaviour
    {
        [Inject] private AnimPanelActivity _activity;

        protected override void OnInitialized()
        {
            _activity.Opened += OnOpen;
            _activity.Closed += OnClose;
        }

        protected override void OnDestroy()
        {
            _activity.Opened -= OnOpen;
            _activity.Closed -= OnClose;
        }

        private void OnOpen()
        {
            Time.timeScale = 0;
        }
        
        private void OnClose()
        {
            Time.timeScale = 1;
        }
    }
}
