using RPG.Util;
using UnityEngine.Playables;
using UnityEngine;

namespace RPG.Core
{
    public class CinematicController : MonoBehaviour
    {
        PlayableDirector playableDirectorCmp;
        Collider colliderCmp;

        private void Awake()
        {
            playableDirectorCmp = GetComponent<PlayableDirector>();
            colliderCmp = GetComponent<Collider>();
        }

        private void Start()
        {
            colliderCmp.enabled = !PlayerPrefsUtil.GetBool(SaveConstants.PLAYED_CUTSCENE);
        }

        private void OnEnable()
        {
            playableDirectorCmp.played += HandlePlayed;
            playableDirectorCmp.stopped += HandleStopped;
        }

        private void OnDisable()
        {
            playableDirectorCmp.played -= HandlePlayed;
            playableDirectorCmp.stopped -= HandleStopped;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG))
                return;

            // NOTE Disabling the Collider component will prevent future triggers
            colliderCmp.enabled = false;

            PlayerPrefsUtil.SetBool(SaveConstants.PLAYED_CUTSCENE, true);

            playableDirectorCmp.Play();
            Debug.Log("player collided!");
        }

        private void HandlePlayed(PlayableDirector pd)
        {
            EventManager.RaiseCutsceneUpdated(true);
        }

        private void HandleStopped(PlayableDirector pd)
        {
            EventManager.RaiseCutsceneUpdated(false);
        }
    }
}
