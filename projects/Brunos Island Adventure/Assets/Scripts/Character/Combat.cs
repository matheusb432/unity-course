using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized]
        public float damage = 0;

        [NonSerialized]
        public bool isAttacking = false;

        private Animator animatorCmp;
        private BubbleEvent bubbleEventCmp;

        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            // NOTE Adding an event handler to the bubbled event delegate, enables multiple different handlers in different
            bubbleEventCmp.OnBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack += HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit += HandleBubbleHit;
        }

        // NOTE Cleaning up event listeners when the component is disabled (destroyed)
        private void OnDisable()
        {
            // ? Removing the event listener by subtracting the handler method
            bubbleEventCmp.OnBubbleStartAttack -= HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack -= HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit -= HandleBubbleHit;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (isAttacking)
                return;

            animatorCmp.SetFloat(Consts.SPEED_ANIMATOR_PARAM, 0);
            animatorCmp.SetTrigger(Consts.ATTACK_ANIMATOR_PARAM);
        }

        // NOTE Handling child object events
        private void HandleBubbleStartAttack()
        {
            isAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            isAttacking = false;
        }

        private void HandleBubbleHit()
        {
            // ? Creating a box cast that travels 1 unit forward
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward,
                transform.localScale / 2,
                transform.forward,
                transform.rotation,
                1
            );

            // TODO - refactor?
            foreach (var target in targets)
            {
                // ? Agent must not hit itself
                if (CompareTag(target.transform.tag))
                    continue;

                var healthCmp = target.transform.gameObject.GetComponent<Health>();

                // ? Agent without health can't be attacked
                if (healthCmp == null)
                    continue;

                healthCmp.TakeDamage(damage);
            }
        }

        public void CancelAttack()
        {
            animatorCmp.ResetTrigger(Consts.ATTACK_ANIMATOR_PARAM);
        }
    }
}
