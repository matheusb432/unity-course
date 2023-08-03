using RPG.Util;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Core
{
    public class CinematicController : MonoBehaviour
    {
        private PlayableDirector playableDirectorCmp;
        private Collider colliderCmp;

        private void Awake()
        {
            playableDirectorCmp = GetComponent<PlayableDirector>();
            colliderCmp = GetComponent<Collider>();
        }

        private void Start()
        {
            if (playableDirectorCmp.playOnAwake)
            {
                // ? collider is not necessary if the cutscene plays on awake
                colliderCmp.enabled = false;
                // ? The event must be raised on Start() for GameManager.OnEnable() to handle it
                // ? This is necessary since playing on awake doesn't raise the PlayableDirector.played event
                HandlePlayed(playableDirectorCmp);
                return;
            }
            colliderCmp.enabled = !PlayerPrefsUtil.GetBool(SaveConsts.PLAYED_CUTSCENE);
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
            if (!other.CompareTag(Consts.PLAYER_TAG))
                return;

            // NOTE Disabling the Collider component will prevent future triggers
            colliderCmp.enabled = false;

            PlayerPrefsUtil.SetBool(SaveConsts.PLAYED_CUTSCENE, true);

            playableDirectorCmp.Play();
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
