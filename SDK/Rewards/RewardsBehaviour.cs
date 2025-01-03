using System;
using Zenject;

namespace SDK.Rewards
{
    public abstract class RewardsBehaviour : IInitializable
    {
        public event Action OnRewarded;
        
        protected void ShowReward()
        {
            RewardController.ShowReward(GetType().Name, () => GiveReward());
        }

        private void GiveReward()
        {
            Rewarded();
            OnRewarded?.Invoke();
        }
        
        public abstract void Initialize();
        protected abstract void Rewarded();
    }
}
