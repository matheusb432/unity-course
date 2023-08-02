using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Audio
{
    public class AttackSoundEffect : MonoBehaviour
    {
        AudioSource audioSourceCmp;

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
