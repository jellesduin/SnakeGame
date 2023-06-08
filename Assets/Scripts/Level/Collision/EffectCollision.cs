using Level.Effect;
using Level.Spawner;
using Manager;
using UnityEngine;

namespace Level.Collision
{
    public class EffectCollision : MonoBehaviour
    {
        [SerializeField] private Effects effect;
        private LevelManager _levelManager;
        private BoosterSpawner _boosterSpawner;
        void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _boosterSpawner = transform.parent.GetComponent<BoosterSpawner>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Snake"))
            {
                var effectController = _levelManager.ActiveLevelController.EffectController;
                effectController.ApplyEffect(effect);
                _boosterSpawner.Spawn();
                Destroy(gameObject);
            }
        }
    }
}
