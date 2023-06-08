using System.Collections;
using System.Collections.Generic;
using Level.Models;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Level.Controllers
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private Transform snakeBody;
        [SerializeField] private Transform snakeHead;
        [SerializeField] private int snakeLength = 5;
        
        private PlayerController _playerController;
        private Snake _snake;
        private readonly Dictionary<Vector3, Quaternion> _headRotation = new();
        private bool _isGameOver = false;
        private int _width;
        private int _height;
        public int FoodEaten { get; private set; }

        public void Invert(int duration)
        {
            if (_playerController.Inverted) return;
            _playerController.Inverted = true;
            StartCoroutine(InvertCoroutine(duration));
        }

        private IEnumerator InvertCoroutine(int duration)
        {
            yield return new WaitForSeconds(duration);
            _playerController.Inverted = false;
        }
        
        private void Start()
        {
            _playerController = transform.AddComponent<PlayerController>();

            {
                var levelBuilder = FindObjectOfType<LevelBuilder>();
                _width = levelBuilder.width;
                _height = levelBuilder.height;
            }

            _snake = new Snake(10, 2);
        
            _headRotation.Add(Vector3.up, Quaternion.Euler(0, 0, 180));
            _headRotation.Add(Vector3.down, Quaternion.Euler(0, 0, 0));
            _headRotation.Add(Vector3.right, Quaternion.Euler(0, 0, 90));
            _headRotation.Add(Vector3.left, Quaternion.Euler(0, 0, 270));
            
            SetupSnake(new Vector3(_width / 2f - .5f, _height / 2f - .5f, -1));
        }
        
        public Snake GetSnake() {
            return _snake;
        }
    
        private void Update()
        {
            if (_isGameOver) return;
            RotateSnake();
            MoveForward();
        }
    
        private void SetupSnake(Vector3 startPosition)
        {
            _snake.Direction = Vector3.up;
            _playerController.SetLastDirection(_snake.Direction);
        
            _snake.SnakeParts.Add(Instantiate(snakeHead, startPosition, Quaternion.Euler(0, 0, 180), transform));
            _snake.NextPositions.Add(_snake.Head.position + _snake.Direction);
        
            for (int i = 1; i < snakeLength; i++)
            {
                _snake.SnakeParts.Add(Instantiate(snakeBody, new Vector3(startPosition.x, startPosition.y - i, startPosition.z), Quaternion.Euler(0, 0, 180), transform));
                _snake.NextPositions.Add(_snake.SnakeParts[i].position + _snake.Direction);
            }
            
            RotateHead(_snake.Direction);
        }

        private void RotateSnake()
        {
            // check if the snake is close to the next position
            if (Vector3.Distance(_snake.Head.position, _snake.NextPositions[0]) < 0.01f)
            {
                _playerController.SetLastDirection(_snake.Direction);
                _snake.Direction = _playerController.GetInputDirection();
                // move all the next positions one step forward
                for (int i = _snake.NextPositions.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                        _snake.NextPositions[i] = _snake.Head.position + _snake.Direction;
                    else
                        _snake.NextPositions[i] = _snake.NextPositions[i - 1];
                }
            
                RotateHead(_snake.Direction);
            }
        }

        private void MoveForward()
        {
            // move based on the next positions
            for (int i = 0; i < _snake.SnakeParts.Count; i++)
            {
                _snake.SnakeParts[i].position = Vector3.MoveTowards(_snake.SnakeParts[i].position, _snake.NextPositions[i], _snake.SnakeSpeed * Time.deltaTime);
            }
        }

        private void RotateHead(Vector3 direction)
        {
            _snake.Head.rotation = _headRotation[direction];
        }

        public void Grow()
        {
            if(_snake == null) return;
            if (_snake.SnakeParts.Count >= 1)
            {
                FoodEaten++;
                _snake.SnakeParts.Add(Instantiate(snakeBody, _snake.NextPositions[^1], Quaternion.identity, transform));
                _snake.NextPositions.Add(_snake.NextPositions[^1]);
            }
        }

        public void Shrink()
        {
            if (_snake.SnakeParts.Count <= 1)
            {
                Die();
                return;
            }
            
            Destroy(_snake.SnakeParts[^1].gameObject);
            _snake.SnakeParts.RemoveAt(_snake.SnakeParts.Count - 1);
            _snake.NextPositions.RemoveAt(_snake.NextPositions.Count - 1);
        }
    
        public void Die()
        {
            _snake.Lives--;
            StopAllCoroutines();
            _snake.SnakeSpeed = 10f;
            
            if (_snake.Lives <= 0)
            {
                _isGameOver = true;
                _snake.SnakeSpeed = 0f;
                // remove the snake parts from the back to the front so the head is removed last each part takes 1 second to remove
                StartCoroutine(DelayedDestroy(.25f));

                _snake.NextPositions.Clear();
            
                GameObject.Find("Level").GetComponent<LevelController>().GameOver();
            }
            else
            {
                ResetSnake(new Vector3(10, 10, -1));
            }
        }

        private void ResetSnake(Vector3 location)
        {
            // reset the snakes location. Check if the bodyparts fit in the grid and if not, move them to the next row
        
        
            // move the head to the location
            _snake.Head.position = location;
        
            for (int i = 1; i < _snake.SnakeParts.Count; i++) _snake.SnakeParts[i].position = location - new Vector3(0, i, 0);

            // reset the directions
            for (int i = 0; i < _snake.NextPositions.Count; i++)
            {
                if (i == 0)
                    _snake.NextPositions[i] = _snake.Head.position + Vector3.up;
                else
                    _snake.NextPositions[i] = _snake.NextPositions[i - 1];
            }
        
            _snake.Direction = Vector3.up;
            _playerController.SetLastDirection(_snake.Direction);
            _playerController.SetInputDirection(Vector3.up);
            RotateHead(_snake.Direction);

            // set the speed to 0 for a second
            var oldSpeed = _snake.SnakeSpeed;
            _snake.SnakeSpeed = 0f;
            StartCoroutine(ResumeSpeed(oldSpeed));
        
        }

        IEnumerator ResumeSpeed(float oldSpeed)
        {
            yield return new WaitForSeconds(1f);
            _snake.SnakeSpeed = oldSpeed;
        }
    
        IEnumerator DelayedDestroy(float delay)
        {
            for(int i = _snake.SnakeParts.Count - 1; i >= 0; i--)
            {
                Destroy(_snake.SnakeParts[i].gameObject);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}