using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
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
    }
}
