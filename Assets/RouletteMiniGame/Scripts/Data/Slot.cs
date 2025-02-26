namespace RouletteMiniGame.Data
{
    public class Slot
    {
        public int Index;
        public float Weight;
        public RewardDataBase.Reward Reward;
        public int Amount;
        public bool IsActive;
        public bool IsLast;

        public Slot(int index, float weight, RewardDataBase.Reward reward, int amount)
        {
            Index = index;
            Weight = weight;
            Reward = reward;
            Amount = amount;
            IsActive = true;
            IsLast = false;
        }
    }
}