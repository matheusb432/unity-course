using RPG.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public static class SceneTransition
    {
        public static IEnumerator Initiate(int sceneIndex)
        {
            var audioSourceCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<AudioSource>();

            // ? The audio will fade in `volume * audioFadeDurationMultiplier` seconds
            // ? e.g. 0.4 volume and 2 duration => fades in 0.8 seconds
            var audioFadeDurationMultiplier = 1.5f;

            // ? Will smoothly fade out the music by progressively lowering it's volume.
            while (audioSourceCmp.volume > 0)
            {
                audioSourceCmp.volume -= Time.deltaTime / audioFadeDurationMultiplier;

                yield return new WaitForEndOfFrame();
            }
            // ? Runs once the volume is already set to 0
            SceneManager.LoadScene(sceneIndex);
        }

        public static int GetCurrentSceneIndex() => SceneManager.GetActiveScene().buildIndex;
    }
}
