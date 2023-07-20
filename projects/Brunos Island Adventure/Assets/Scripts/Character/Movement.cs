using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    // TODO refactor - seal class?
    // NOTE RequireComponent automatically adds this component to any instance that has this script
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;

        private Vector3 movementVector;

        // NOTE Awake() is called when the script is being loaded
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // NOTE Update() is a method called every frame
        private void Update()
        {
            MovePlayer();
            Rotate();
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
            Vector2 input = context.ReadValue<Vector2>();

            movementVector = new Vector3(input.x, 0, input.y);
        }

        private void Rotate()
        {
            // NOTE No rotation can be applied if movementVector is a vector zero (player is not moving)
            if (movementVector == Vector3.zero)
                return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);

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
        }

        public void SetAgentStopped(bool val) => agent.isStopped = val;

        public void StopMovingAgent()
        {
            agent.ResetPath();
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
        }

        public void UpdateAgentSpeed(float newSpeed)
        {
            agent.speed = newSpeed;
        }
    }
}
