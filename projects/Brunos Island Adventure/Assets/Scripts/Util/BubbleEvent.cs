using UnityEngine;
using UnityEngine.Events;

namespace RPG.Util
{
    public class BubbleEvent : MonoBehaviour
    {
        // NOTE The `event` keyword  prevents this delegate from being overwritten, events can only be added to it with the `+=` operator
        // ? Creating custom events to bubble the events
        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public event UnityAction OnBubbleHit = () => { };
        public event UnityAction OnBubbleCompleteDefeat = () => { };

        // NOTE The method names `OnStartAttack` and `OnCompleteAttack` must match the attack animation's custom event name
        private void OnStartAttack()
        {
            // NOTE .Invoke() will run any methods that are listening for this event
            OnBubbleStartAttack.Invoke();
        }

        private void OnCompleteAttack()
        {
            OnBubbleCompleteAttack.Invoke();
        }

        private void OnHit()
        {
            OnBubbleHit.Invoke();
        }

        private void OnCompleteDefeat()
        {
            OnBubbleCompleteDefeat.Invoke();
        }
    }
}
