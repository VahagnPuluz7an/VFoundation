using System;
using DSystem;
using UnityEngine;
using VFoundation.Tutorial;

namespace SDK
{
    [AutoRegistry]
    public class AdsManager : IInitializable, IUpdatable
    {
        public static event Action Rewarded;

        private static float _timer;
        
        public void Initialize()
        {
            //Subscribe Rewarded Event To Publisher
        }
        
        public void Update()
        {
            _timer += Time.deltaTime;
        }
        
        public static void ShowBanner()
        {
            //ShowBanner
        }

        public static void ShowInter()
        {
            if (_timer > 30 && TutorialManager.TutorialIsCompleted)
            {
                //Show Inter
                _timer = 0;
            }
        }

        public static void ShowRewarded(string name)
        {
            //Show Rewarded
            _timer = 0;
        }
    }
}
