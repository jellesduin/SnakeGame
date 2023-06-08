using System;
using System.Collections;
using Level.Controllers;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Spawner
{
    public class BoosterSpawner : Spawner
    {
        [SerializeField] private GameObject[] prefabs;
        private GameObject _currentBooster;
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
            var rng = Random.Range(0, prefabs.Length);
            _currentBooster = Instantiate(prefabs[rng], transform);
            _currentBooster.transform.position = GetRandomLocation();
            
            StartCoroutine(SpawnNewCoroutine());
        }
        
        private IEnumerator SpawnNewCoroutine()
        {
            if (_currentBooster != null)
            {
                yield return new WaitForSeconds(10);
                Destroy(_currentBooster);
                Spawn();
            }
        }
    }
}
