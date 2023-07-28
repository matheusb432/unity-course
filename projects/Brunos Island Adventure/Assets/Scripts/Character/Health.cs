using RPG.Core;
using UnityEngine.UI;
using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };

        [NonSerialized]
        public float healthPoints = 0;

        [NonSerialized]
        public Slider sliderCmp;

        public float maxHealth = 0;
        private bool isDefeated = false;

        private Animator animatorCmp;
        private BubbleEvent bubbleEventCmp;

        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            sliderCmp = GetComponentInChildren<Slider>();
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
            SetHealth(Mathf.Max(healthPoints - damage, 0));

            if (sliderCmp != null)
                sliderCmp.value = healthPoints;

            if (healthPoints == 0)
                Defeated();
        }

        public void Heal(float health)
        {
            SetHealth(Mathf.Min(healthPoints + health, maxHealth));
        }

        private void SetHealth(float health)
        {
            healthPoints = health;
            // TODO research if CompareTag is slow (seems slow to pointlessly check tag for every point of damage that every enemy takes)
            if (CompareTag(Constants.PLAYER_TAG))
                EventManager.RaiseChangePlayerHealth(healthPoints);
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
