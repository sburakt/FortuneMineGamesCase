using System.Collections.Generic;

namespace RouletteMiniGame.Data
{
    public class RewardDataBase
    {
        public enum Reward
        {
            Beer,
            Candy,
            Coconut,
            Croissant,
            Donut,
            Egg,
            Fig,
            GodFood,
            HotChocolate,
            HotDog,
            Mushroom,
            Noodle,
            Pineapple,
            Shrimp
        }

        private Dictionary<Reward, RewardData> _rewardsData = new Dictionary<Reward, RewardData>();
    }
    
    public class RewardData
    {
        private int id;
        private string name;
    }
}