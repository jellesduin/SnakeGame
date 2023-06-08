using Level.Controllers;
using UnityEngine;

namespace Level.Collision
{
    public class SnakeCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Wall"))
            {
                transform.parent.GetComponent<SnakeController>().Die();
            }

            if (col.CompareTag("Bodypart"))
            {
                transform.parent.GetComponent<SnakeController>().Die();
            }
        }
    }
}