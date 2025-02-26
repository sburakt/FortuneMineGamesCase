using UnityEngine;
using RouletteMiniGame.Data;

namespace RouletteMiniGame.Animations
{
    public class CircleRowScript : MonoBehaviour, ICircleRow
    {
        public Slot Slot { get; private set; }
        ICircleRowAnimations _animations;
        RewardAnimations _rewardAnimations;

        public void SetSlot(Slot slot)
        {
            this.Slot = slot;
            _rewardAnimations.SetReward(slot.Reward);
        }

        public void Awake()
        {
            _animations = GetComponent<ICircleRowAnimations>();
            _rewardAnimations = GetComponentInChildren<RewardAnimations>();
            _animations.SetRewardAnimation(_rewardAnimations);
            _animations.SetTickAnimation(gameObject.GetComponent<TickAnimations>());
        }
    }


    public interface ICircleRow
    {
    }
}