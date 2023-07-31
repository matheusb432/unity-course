using RPG.Core;
using UnityEngine;

namespace RPG.Quests
{
    public class Reward : MonoBehaviour
    {
        [SerializeField]
        private RewardSO reward;
        private bool rewardTaken = false;

        public void SendReward()
        {
            if (rewardTaken)
                return;

            EventManager.RaiseReward(reward);
            rewardTaken = true;
        }
    }
}
