using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        // NOTE [SerializeField] will make this accessible from the unity editor
        [SerializeField]
        private GameObject splineGameObject;

        private SplineContainer splineCmp;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline.");
                return;
            }

            splineCmp = splineGameObject.GetComponent<SplineContainer>();
        }

        public Vector3 GetNextPosition()
        {
            return splineCmp.EvaluatePosition(0);
        }
    }
}
