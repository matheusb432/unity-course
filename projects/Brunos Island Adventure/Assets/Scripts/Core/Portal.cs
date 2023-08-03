using RPG.Util;
using UnityEngine;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private int nextSceneIndex;

        public Transform spawnPoint;

        private Collider colliderCmp;

        private void Awake()
        {
            colliderCmp = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Consts.PLAYER_TAG))
                return;

            colliderCmp.enabled = false;

            EventManager.RaisePortalEnter(other, nextSceneIndex);

            StartCoroutine(SceneTransition.Initiate(nextSceneIndex));
        }
    }
}
