using System.Collections.Generic;
using RouletteMiniGame.Data;
using UnityEngine;

namespace RouletteMiniGame.AssetManagement
{
    public static class RewardSpriteLoader
    {
        private static Dictionary<RewardDataBase.Reward, Sprite> rewardSprites =
            new Dictionary<RewardDataBase.Reward, Sprite>();

        public static void AddReward(RewardDataBase.Reward reward, Sprite rewardSprite)
        {
            rewardSprites[reward] = rewardSprite;
        }

        public static Sprite GetSprite(RewardDataBase.Reward reward)
        {
            return rewardSprites[reward];
        }
    }
}