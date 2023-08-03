using Assets.Scripts.Character;
using RPG.Util;
using System;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        // NOTE [NonSerialized] will make this variable not visible in the Unity Editor, even though it's public
        [NonSerialized]
        public Vector3 originalPosition;

        [NonSerialized]
        public Movement movementCmp;

        [NonSerialized]
        public GameObject player;

        [NonSerialized]
        public Patrol patrolCmp;

        private Health healthCmp;

        [NonSerialized]
        public Combat combatCmp;

        public CharacterStatsSO stats;
        public string enemyId = string.Empty;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        public bool IsPlayerInChaseRange => DistanceFromPlayer <= chaseRange;
        public bool IsPlayerInAttackRange => DistanceFromPlayer <= attackRange;

        public float DistanceFromPlayer { get; private set; }

        private IAIState currentState;
        public AIReturnState returnState = new();
        public AIChaseState chaseState = new();
        public AIAttackState attackState = new();
        public AIPatrolState patrolState = new();
        public AIDefeatedState defeatedState = new();

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }

            if (string.IsNullOrEmpty(enemyId))
            {
                Debug.LogWarning($"{name} does not have an enemyId.");
            }

            currentState = returnState;
            // NOTE FindWithTag() can be slow and should be used carefully
            player = GameObject.FindWithTag(Consts.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        // NOTE The Start() method is called after Awake(), when the component is enabled (just before any Update method is called)
        private void Start()
        {
            currentState.EnterState(this);

            healthCmp.healthPoints = healthCmp.maxHealth = stats.health;
            combatCmp.damage = stats.damage;

            if (healthCmp.sliderCmp != null)
            {
                healthCmp.sliderCmp.maxValue = healthCmp.maxHealth;
                healthCmp.sliderCmp.value = healthCmp.maxHealth;
            }
        }

        private void OnEnable()
        {
            healthCmp.OnStartDefeated += HandleStartDefeated;
        }

        private void OnDisable()
        {
            healthCmp.OnStartDefeated -= HandleStartDefeated;
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(IAIState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null)
                return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            // NOTE Drawing the chase range gizmo to help visual debugging, doesn't change the game itself.
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        private void HandleStartDefeated()
        {
            SwitchState(defeatedState);
        }
    }
}
