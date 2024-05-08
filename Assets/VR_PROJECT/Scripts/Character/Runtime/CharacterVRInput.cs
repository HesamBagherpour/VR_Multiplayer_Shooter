using UnityEngine;
using FishNet.Object;
using UnityEngine.Animations;

namespace VR_PROJECT.Character
{
    public class CharacterVRInput : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private void Update()
        {
            Move();
            Jump();
            Rotate();
        }

        private void Jump()
        {
            bool Jumped = OVRInput.Get(OVRInput.Button.Three);
            jump = Jumped;
        }

        private void Move()
        {
            Vector2 leftThumbstick = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            bool _isRunning = OVRInput.Get(OVRInput.RawButton.LThumbstick);
            sprint = _isRunning;
            move = leftThumbstick;
        }

        private void Rotate()
        {
            Vector2 rightThumbstick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
            look = rightThumbstick;
        }

        public Vector3 GetMovementDirection(Vector2 input)
        {
            float moveInput = input.y;
            Vector3 leftRightMovement = transform.right * input.x;

            Vector3 movement = leftRightMovement + transform.forward * moveInput;

            return movement;
        }

        public float GetRotationAngle(Vector2 input)
        {
            return input.x;
        }
    }
}