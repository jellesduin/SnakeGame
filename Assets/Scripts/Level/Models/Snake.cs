using System.Collections.Generic;
using UnityEngine;

namespace Level.Models
{
    public class Snake
    {
        public Snake(float speed, int lives)
        {
            SnakeSpeed = speed;
            Lives = lives;
            BaseSpeed = speed;
        }

        public readonly float BaseSpeed;
        public float SnakeSpeed { get; set; }
        public int Lives { get; set; }
        public Vector3 Direction { get; set; }
        public List<Transform> SnakeParts { get; } = new();
        public List<Vector3> NextPositions { get; } = new();
        public Transform Head => SnakeParts[0];
        public int Length => SnakeParts.Count;
    }
}