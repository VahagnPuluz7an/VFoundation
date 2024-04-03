using System;
using DSystem;

namespace SDK
{
    [AutoRegistry]
    public abstract class RewardController : IInitializable
    {
        private static event Action OnSuccess;

        public void Initialize()
        {
            AdsManager.Rewarded += Completed;
        }
        
        public static void ShowReward(string anal, Action completed)
        {
            OnSuccess = () => completed?.Invoke();
            AdsManager.ShowRewarded(anal);
        }
        
        private static void Completed()
        {
            OnSuccess?.Invoke();
            OnSuccess = null;
        }
    }
}
