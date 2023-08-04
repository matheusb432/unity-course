using UnityEngine;

namespace RPG.Audio
{
    public sealed class AttackSoundEffect : MonoBehaviour
    {
        private AudioSource audioSourceCmp;

        private void Awake()
        {
            audioSourceCmp = GetComponent<AudioSource>();
        }

        private void OnStartAttack()
        {
            if (audioSourceCmp.clip == null)
                return;

            audioSourceCmp.Play();
        }
    }
}
