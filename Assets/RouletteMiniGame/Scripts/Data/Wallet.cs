using System.Collections.Generic;

namespace RouletteMiniGame.Data
{
    public class Wallet
    {
        private Dictionary<RewardDataBase.Reward, int> _rewardAmountDictionary;

        public Wallet()
        {
            _rewardAmountDictionary = new Dictionary<RewardDataBase.Reward, int>();
        }

        public Wallet(Dictionary<RewardDataBase.Reward, int> rewardAmountDictionary)
        {
            _rewardAmountDictionary = rewardAmountDictionary;
        }

        public Dictionary<RewardDataBase.Reward, int> GetRewardAmountDictionary()
        {
            return _rewardAmountDictionary;
        }

        public void AddReward(RewardDataBase.Reward reward, int amount)
        {
            if (_rewardAmountDictionary.ContainsKey(reward))
                _rewardAmountDictionary[reward] += amount;
            else
            {
                _rewardAmountDictionary.Add(reward, amount);
            }
        }
    }
}