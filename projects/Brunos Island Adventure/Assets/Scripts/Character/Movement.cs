using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    // NOTE RequireComponent automatically adds this component to any instance that has this script
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Movement : MonoBehaviour
    {
        [NonSerialized]
        public Vector3 originalForwardVector;

        [NonSerialized]
        public bool isMoving = false;
        private bool sprintActive = false;

        private NavMeshAgent agent;
        private Animator animatorCmp;

        [SerializeField]
        private float sprintMult = 2f;

        private Vector3 movementVector;
        private bool clampAnimatorSpeedAgain = true;

        // NOTE Awake() is called when the script is being loaded
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            // NOTE GetComponentInChildren searches for a component from within child objects of the current game object
            animatorCmp = GetComponentInChildren<Animator>();
            // NOTE transform.forward gets the vector representing the blue axis of the game object, which is where it's facing
            originalForwardVector = transform.forward;
        }

        private void Start()
        {
            // NOTE Disables default unity handling of rotation
            agent.updateRotation = false;
        }

        // NOTE Update() is a method called every frame
        private void Update()
        {
            MovePlayer();

            MovementAnimator();

            if (CompareTag(Consts.PLAYER_TAG))
                Rotate(movementVector);
        }

        private void MovePlayer()
        {
            // NOTE `deltaTime` gets the interval in seconds from the last frame to the current one
            // ? This is necessary to implement Framerate Independence, so that the game speed is not tied to the FPS of a player
            // NOTE The vector should come as the last operand since vector math is more expensive than floating-point math
            Vector3 offset = agent.speed * Time.deltaTime * movementVector;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                isMoving = true;
            if (context.canceled)
                isMoving = false;

            Vector2 input = context.ReadValue<Vector2>();

            movementVector = new Vector3(input.x, 0, input.y);
        }

        public void HandleSprint(InputAction.CallbackContext context)
        {
            if (!context.performed || context.canceled)
            {
                if (sprintActive)
                    UpdateAgentSpeed(agent.speed / sprintMult, false);
                sprintActive = false;

                return;
            }

            UpdateAgentSpeed(agent.speed * sprintMult, false);
            sprintActive = true;
        }

        public void Rotate(Vector3 newForwardVector)
        {
            // NOTE No rotation can be applied if movementVector is a vector zero (player is not moving)
            if (newForwardVector == Vector3.zero)
                return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newForwardVector);

            // ? Calculates the linear interpolation (Lerp) based on the time between frames (deltaTime) to smoothly rotate the player (agent)
            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            isMoving = true;
        }

        public void SetAgentStopped(bool val) => agent.isStopped = val;

        public void StopMovingAgent()
        {
            agent.ResetPath();
            isMoving = false;
        }

        public bool ReachedDestination()
        {
            if (agent.pathPending)
                return false;
            if (agent.remainingDistance > agent.stoppingDistance)
                return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0)
                return false;

            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
            isMoving = true;
        }

        public void UpdateAgentSpeed(float newSpeed, bool shouldClampSpeed)
        {
            agent.speed = newSpeed;

            clampAnimatorSpeedAgain = shouldClampSpeed;
        }

        public void ResetRotate()
        {
            Rotate(originalForwardVector);
        }

        private void MovementAnimator()
        {
            float speed = animatorCmp.GetFloat(Consts.SPEED_ANIMATOR_PARAM);
            float smoothening = Time.deltaTime * agent.acceleration;

            speed += isMoving ? smoothening : -smoothening;

            // NOTE Mathf.Clamp01() simply clamps the value with min = 0 and max = 1
            speed = Mathf.Clamp01(speed);

            // ? If CompareTag(Constants.ENEMY_TAG) is true, then the game object is an Enemy
            if (CompareTag(Consts.ENEMY_TAG) && clampAnimatorSpeedAgain)
            {
                // ? Clamping the speed to 0.5, which is the walking speed
                speed = Mathf.Clamp(speed, 0, 0.5f);
            }

            animatorCmp.SetFloat(Consts.SPEED_ANIMATOR_PARAM, speed);
        }
    }
}
