using RPG.Core;
using UnityEngine;

namespace RPG.Quests
{
    public sealed class Reward : MonoBehaviour
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

            var audioSourceCmp = GetComponent<AudioSource>();
            if (audioSourceCmp.clip != null)
                audioSourceCmp.Play();
        }
    }
}
