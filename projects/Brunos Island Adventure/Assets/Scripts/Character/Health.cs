using System;
using UnityEngine.Events;
using RPG.Util;
using UnityEngine;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };

        [NonSerialized]
        public float healthPoints = 0;
        private bool isDefeated = false;

        private Animator animatorCmp;
        private BubbleEvent bubbleEventCmp;

        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        private void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
                Defeated();
        }

        private void Defeated()
        {
            if (isDefeated)
                return;

            if (CompareTag(Constants.ENEMY_TAG))
            {
                // NOTE Emitting a custom event
                OnStartDefeated.Invoke();
            }

            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }

        private void HandleBubbleCompleteDefeat()
        {
            // NOTE Destroying the game object that this component is attached to
            Destroy(gameObject);
        }
    }
}
