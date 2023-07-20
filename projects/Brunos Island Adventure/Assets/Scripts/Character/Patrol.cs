using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        // NOTE [SerializeField] will make this accessible from the unity editor
        [SerializeField]
        private GameObject splineGameObject;

        [SerializeField]
        private float walkDuration = 3;

        [SerializeField]
        private float pauseDuration = 2;

        private SplineContainer splineCmp;
        private NavMeshAgent agentCmp;

        private float splinePosition = 0;
        private float splineLength = 0;
        private float lengthWalked = 0;
        private float walkTime = 0;
        private float pauseTime = 0;
        private bool isWalking = true;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline.");
                return;
            }

            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
            agentCmp = GetComponent<NavMeshAgent>();
        }

        public Vector3 GetNextPosition()
        {
            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPosition()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration)
                isWalking = false;

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;
                if (pauseTime < pauseDuration)
                    return;

                ResetTimers();
            }

            lengthWalked += Time.deltaTime * agentCmp.speed;

            if (lengthWalked > splineLength)
                lengthWalked = 0;

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0;
            walkTime = 0;
            isWalking = true;
        }
    }
}
