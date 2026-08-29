using UnityEngine;
using UnityEngine.Pool;

namespace KhosaryCode.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 15f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float lifetime = 3f;

        private Vector2 _direction;
        private float _lifetimeTimer;
        private IObjectPool<Projectile> _pool;
        private int _obstacleLayer;

        private void Awake()
        {
            // Cache the layer to avoid NameToLayer string allocations on every collision
            _obstacleLayer = LayerMask.NameToLayer("Obstacles");
            if (_obstacleLayer == -1) _obstacleLayer = LayerMask.NameToLayer("Walls");
        }

        public void Initialize(Vector2 direction, IObjectPool<Projectile> pool)
        {
            _direction = direction.normalized;
            _pool = pool;
            _lifetimeTimer = 0f;
            
            // Rotate to face direction
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Update()
        {
            transform.Translate(_direction * (speed * Time.deltaTime), Space.World);

            _lifetimeTimer += Time.deltaTime;
            if (_lifetimeTimer >= lifetime)
            {
                ReleaseToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check for IDamagable (currently PlayerHealth)
            IDamagable damagable = collision.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
                ReleaseToPool();
                return;
            }
            
            // Destroy on walls
            if (collision.gameObject.layer == _obstacleLayer)
            {
                ReleaseToPool();
            }
        }

        private void ReleaseToPool()
        {
            if (_pool != null && gameObject.activeSelf)
            {
                _pool.Release(this);
            }
            else if (gameObject.activeSelf)
            {
                Destroy(gameObject);
            }
        }
    }
}
