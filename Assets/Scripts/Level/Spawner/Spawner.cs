using System.Runtime.CompilerServices;
using Level.Controllers;
using UnityEngine;

namespace Level.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        protected LevelController LevelController;
        protected int Width;
        protected int Height;

        protected Vector3 GetRandomLocation()
        {
            var rngLocation = new Vector3(Random.Range(0, Width), Random.Range(0, Height), -2);
            var walls = LevelController.GetWalls();
            var rngBounds = new Bounds(rngLocation + new Vector3(0.5f, 0.5f, 0), Vector3.one * 0.5f);

            Debug.Log(walls.Length);
            
            foreach (var wall in walls)
            {
                var bounds = wall.GetComponent<BoxCollider2D>().bounds;
                if (bounds.Intersects(rngBounds))
                {
                    Debug.Log("Intersect");
                    return GetRandomLocation();
                }
            }
            
            return rngLocation;
        }

        public abstract void Spawn();
    }
}