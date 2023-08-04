using UnityEngine;

namespace RPG.UI
{
    public sealed class CameraRotator : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private void Update()
        {
            transform.Rotate(0, Time.deltaTime * speed, 0);
        }
    }
}
