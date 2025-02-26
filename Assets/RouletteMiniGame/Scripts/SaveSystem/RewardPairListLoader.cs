using System;
using System.Collections.Generic;
using System.IO;
using RouletteMiniGame.Data;
using UnityEngine;


namespace RouletteMiniGame.SaveSystem
{
    [Serializable]
    public class RewardPairList
    {
        public List<RewardAmountPair> pairs;

        [Serializable]
        public class RewardAmountPair
        {
            public RewardDataBase.Reward reward;
            public int amount;
        }

        public RewardPairList()
        {
            pairs = new List<RewardAmountPair>();
        }

        public static RewardPairList GetRewardPairListByDictionary(Dictionary<RewardDataBase.Reward, int> dictionary)
        {
            List<RewardAmountPair> pairList = new List<RewardAmountPair>();
            foreach (var keyValuePair in dictionary)
            {
                RewardAmountPair pair = new RewardAmountPair();
                pair.reward = keyValuePair.Key;
                pair.amount = keyValuePair.Value;
                pairList.Add(pair);
            }

            RewardPairList rewardPairList = new RewardPairList();
            rewardPairList.pairs = pairList;
            return rewardPairList;
        }

        public static Dictionary<RewardDataBase.Reward, int> GetDictionaryByRewardPairList(RewardPairList pairList)
        {
            Dictionary<RewardDataBase.Reward, int> dictionary = new Dictionary<RewardDataBase.Reward, int>();
            foreach (var rewardAmountPair in pairList.pairs)
            {
                dictionary.Add(rewardAmountPair.reward , rewardAmountPair.amount);
            }
            return dictionary;
        }
    }


    public class RewardPairListLoader
    {
        private string _filePath;

        public RewardPairListLoader(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void SaveList(RewardPairList rewardPairList)
        {
            string json = JsonUtility.ToJson(rewardPairList);
            File.WriteAllText(_filePath, json);
        }

        public RewardPairList LoadList()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                return JsonUtility.FromJson<RewardPairList>(json);
            }

            return new RewardPairList();
        }

        public void DeleteSave()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}