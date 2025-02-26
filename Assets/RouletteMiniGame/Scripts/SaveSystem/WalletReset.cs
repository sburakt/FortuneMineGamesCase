using UnityEngine;
using UnityEngine.UI;

namespace RouletteMiniGame.SaveSystem
{
    public class WalletReset : MonoBehaviour
    {
        [SerializeField] private Button nextSceneButton;

        private void Start()
        {
            nextSceneButton.onClick.AddListener(ResetWallet);
        }

        public void ResetWallet()
        {
            RewardPairListLoader rewardPairListLoader = new RewardPairListLoader("wallet.json");
            rewardPairListLoader.DeleteSave();
        }

    }
}
