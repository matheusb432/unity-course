﻿using UnityEngine;

namespace RPG.Util
{
    // ? A billboard is a game object that always faces the camera
    public sealed class Billboard : MonoBehaviour
    {
        private GameObject cam;

        private void Awake()
        {
            cam = GameObject.FindGameObjectWithTag(Consts.CAMERA_TAG);
        }

        // NOTE LateUpdate() runs after Update(), this guarantees that this runs after the camera updates and prevents race conditions
        private void LateUpdate()
        {
            var cameraDirection = transform.position + cam.transform.forward;

            transform.LookAt(cameraDirection);
        }
    }
}
