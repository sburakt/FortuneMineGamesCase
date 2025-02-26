using System.Collections.Generic;
using RouletteMiniGame.Data;
using RouletteMiniGame.Events;
using RouletteMiniGame.EventSystem;
using RouletteMiniGame.SaveSystem;

public class AssetConfig
{
    public Dictionary<int, string> RewardSpritesPaths;
    public string SceneBuilderPrefabPath;
    public string CircleRowPrefabPath;
}

namespace RouletteMiniGame.MVP
{
    public class Model
    {
        private RewardPairListLoader _rewardPairListLoader;
        public Slot[] slots;
        public Wallet PlayerWallet;
        private RewardDataBase.Reward reward;
        public AssetConfig AssetConfig;

        public Model()
        {
            _rewardPairListLoader = new RewardPairListLoader("wallet.json");
            PlayerWallet = new Wallet(RewardPairList.GetDictionaryByRewardPairList(_rewardPairListLoader.LoadList()));
            InitializeAssetConfig();
            PopulateSlots();
            Subscribe();
        }

        private void Subscribe()
        {
            GlobalEventSystem.Instance.Subscribe<UnityGameEvents.SceneUnloadEvent>(OnSceneUnloaded);
        }

        private void Unsubscribe()
        {
            GlobalEventSystem.Instance.Unsubscribe<UnityGameEvents.SceneUnloadEvent>(OnSceneUnloaded);
        }

        private void OnSceneUnloaded(Events.UnityGameEvents.SceneUnloadEvent e)
        {
            RewardPairList rewardPairList = RewardPairList.GetRewardPairListByDictionary(PlayerWallet.GetRewardAmountDictionary());
            _rewardPairListLoader.SaveList(rewardPairList);
            Unsubscribe();
        }

        private void InitializeAssetConfig()
        {
            AssetConfig = new AssetConfig();
            AssetConfig.RewardSpritesPaths = new Dictionary<int, string>
            {
                { 0, "Sprites/Rewards/Beer" },
                { 1, "Sprites/Rewards/Candy" },
                { 2, "Sprites/Rewards/Coconut" },
                { 3, "Sprites/Rewards/Croissant" },
                { 4, "Sprites/Rewards/Donut" },
                { 5, "Sprites/Rewards/Egg" },
                { 6, "Sprites/Rewards/Fig" },
                { 7, "Sprites/Rewards/God-Food" },
                { 8, "Sprites/Rewards/Hot-Chocolate" },
                { 9, "Sprites/Rewards/Hot-Dog" },
                { 10, "Sprites/Rewards/Mushroom" },
                { 11, "Sprites/Rewards/Noodle" },
                { 12, "Sprites/Rewards/Pineapple" },
                { 13, "Sprites/Rewards/Shrimp" }
            };
            AssetConfig.SceneBuilderPrefabPath = "Prefabs/SceneBuilder";
            AssetConfig.CircleRowPrefabPath = "Prefabs/CuisinePartyCircleRow";
        }

        public void PopulateSlots()
        {
            slots = new Slot[14];
            for (int i = 0; i < 14; i++)
            {
                slots[i] = new Slot(i, 1, (RewardDataBase.Reward)i, 1);
            }
        }
    }
    
    public static class ModelLocator
    {
        private static Model _model;

        public static void SetModel(Model model)
        {
            _model = model;
        }

        public static Model GetModel()
        {
            return _model;
        }
    }
}