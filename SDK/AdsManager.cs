using System;
using UnityEngine;
using Zenject;

namespace SDK
{
    public class AdsManager : IInitializable, ITickable
    {
        public static event Action Rewarded;

        private static float _timer;
        
        public void Initialize()
        {
            //Subscribe Rewarded Event To Publisher
        }
        
        public void Tick()
        {
            _timer += Time.deltaTime;
        }
        
        public static void ShowBanner()
        {
            //ShowBanner
        }

        public static void ShowInter()
        {
            if (_timer > 30)
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
