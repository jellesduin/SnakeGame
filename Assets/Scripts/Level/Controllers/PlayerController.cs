using System;
using UnityEngine;

namespace Level.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 _inputDirection = Vector3.up;
        private Vector3 _lastDirection;
        public bool Inverted { get; set; } = false;

        public SnakeController SnakeController { get; set; }
        private void Awake()
        {
            SnakeController = GetComponent<SnakeController>();
        }

        private void Update()
        {
            MoveInput();
        }

        private void MoveInput()
        {
            if (Inverted)
            {
                InvertedMove();
            }
            else
            {
                NormalMove();
            }
        }

        private void InvertedMove()
        {
            if (Input.GetKey(KeyCode.W) && _lastDirection != Vector3.down)
            {
                _inputDirection = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.S) && _lastDirection != Vector3.up)
            {
                _inputDirection = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.A) && _lastDirection != Vector3.left)
            {
                _inputDirection = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.D) && _lastDirection != Vector3.right)
            {
                _inputDirection = Vector3.left;
            }
        }

        private void NormalMove()
        {
            if (Input.GetKey(KeyCode.W) && _lastDirection != Vector3.down)
            {
                _inputDirection = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.S) && _lastDirection != Vector3.up)
            {
                _inputDirection = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.A) && _lastDirection != Vector3.right)
            {
                _inputDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D) && _lastDirection != Vector3.left)
            {
                _inputDirection = Vector3.right;
            }
        }
    
        public Vector3 GetInputDirection() {
            return _inputDirection;
        }

        public void SetLastDirection(Vector3 lastDirection) {
            _lastDirection = lastDirection;
        }

        public void SetInputDirection(Vector3 input)
        {
            _inputDirection = input;
        }
    }
}