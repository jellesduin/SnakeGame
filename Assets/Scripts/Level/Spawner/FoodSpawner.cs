using Level.Controllers;
using UnityEngine;

namespace Level.Spawner
{
    public class FoodSpawner : Spawner
    {
        [SerializeField] private GameObject prefab;
        void Start()
        {
            LevelController = transform.parent.GetComponent<LevelController>();
            LevelController.GetLevelSize(out var w, out var h);
            Width = w;
            Height = h;
            
            Spawn();
        }

        public override void Spawn()
        {
            var item = Instantiate(prefab, transform);
            item.transform.position = GetRandomLocation();
        }
    }
}
