using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Controllers
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Sprite[] groundTiles;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject snakePrefab;
        [SerializeField] private Camera mainCamera;

        private GameObject[,] _map;
        
        [Header("Level Settings")]
        public int width = 25;
        public int height = 25;

        private void Start()
        {
            _map = new GameObject[width, height];
            mainCamera.transform.position = new Vector3(width / 2f - .5f, height / 2f - .5f, -10);
            mainCamera.orthographicSize = Mathf.Max(width, height) / 2f + 1;

            GenerateMap();
            GenerateWalls();
        }

        void GenerateMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tilePrefab.transform.position = new Vector3(x, y, 0);
                    tilePrefab.GetComponent<SpriteRenderer>().sprite = groundTiles[Random.Range(0, groundTiles.Length)];
                    _map[x, y] = Instantiate(tilePrefab, transform, true);
                }
            }
        }
        
        void GenerateWalls()
        {
            var wall1 = Instantiate(wallPrefab, transform, true);
            wall1.transform.position = new Vector3(width / 2f - .5f, height, 0);
            wall1.transform.localScale = new Vector3(width + 2, 1, 1);

            var wall2 = Instantiate(wallPrefab, transform, true);
            wall2.transform.position = new Vector3(width / 2f - .5f, -1, 0);
            wall2.transform.localScale = new Vector3(width + 2, 1, 1);
            
            var wall3 = Instantiate(wallPrefab, transform, true);
            wall3.transform.position = new Vector3(-1, height / 2f - .5f, 0);
            wall3.transform.localScale = new Vector3(1, height + 2, 1);
            
            var wall4 = Instantiate(wallPrefab, transform, true);
            wall4.transform.position = new Vector3(width, height / 2f - .5f, 0);
            wall4.transform.localScale = new Vector3(1, height + 2, 1);
        }
    }
}