using Assets.Scripts.Character;
using RPG.Util;
using System;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        // NOTE [NonSerialized] will make this variable not visible in the Unity Editor, even though it's public
        // TODO refactor - from field to property?
        [NonSerialized]
        public float distanceFromPlayer;

        // TODO refactor - init only prop?
        [NonSerialized]
        public Vector3 originalPosition;

        [NonSerialized]
        public Movement movementCmp;

        [NonSerialized]
        public GameObject player;

        public Patrol patrolCmp;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        public bool IsPlayerInChaseRange => distanceFromPlayer <= chaseRange;
        public bool IsPlayerInAttackRange => distanceFromPlayer <= attackRange;

        private AIBaseState currentState;
        public AIReturnState returnState = new();
        public AIChaseState chaseState = new();
        public AIAttackState attackState = new();
        public AIPatrolState patrolState = new();

        private void Awake()
        {
            currentState = returnState;
            // NOTE FindWithTag() can be slow and should be used carefully
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();

            originalPosition = transform.position;
        }

        // NOTE The Start() method is called after Awake(), when the component is enabled (just before any Update method is called)
        private void Start()
        {
            currentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            // NOTE Equivalent to `ReferenceEquals(newState, currentState)` in this instance, compares the pointers instead of the values themselves
            // ? This way, no unnecessary state initializations are performed, can be useful if this can happen and they're expensive inits
            //if (currentState == newState)
            //    return;

            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null)
                return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            // NOTE Drawing the chase range gizmo to help visual debugging, doesn't change the game itself.
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
