using UnityEngine;
using UnityEngine.Pool;

namespace KhosaryCode.Combat
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [Header("Weapon Settings")]
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _defaultPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 30;

        private IObjectPool<Projectile> _projectilePool;

        private void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>(
                CreateProjectile,
                OnGetProjectile,
                OnReleaseProjectile,
                OnDestroyProjectile,
                false,
                _defaultPoolSize,
                _maxPoolSize
            );

            if (_firePoint == null)
            {
                _firePoint = transform; // Default to self if not assigned
            }
        }

        private Projectile CreateProjectile()
        {
            if (_projectilePrefab == null)
            {
                Debug.LogError($"[{gameObject.name}] ProjectileWeapon missing Projectile Prefab!");
                return null;
            }

            Projectile proj = Instantiate(_projectilePrefab);
            return proj;
        }

        private void OnGetProjectile(Projectile proj)
        {
            if (proj != null)
            {
                proj.gameObject.SetActive(true);
            }
        }

        private void OnReleaseProjectile(Projectile proj)
        {
            if (proj != null)
            {
                proj.gameObject.SetActive(false);
            }
        }

        private void OnDestroyProjectile(Projectile proj)
        {
            if (proj != null)
            {
                Destroy(proj.gameObject);
            }
        }

        public void Fire(Vector2 direction)
        {
            if (_projectilePrefab == null) return;

            Projectile proj = _projectilePool.Get();
            if (proj != null)
            {
                proj.transform.position = _firePoint.position;
                proj.Initialize(direction, _projectilePool);
                
                KhosaryCode.VisualFeedbacks.VFXManager.Instance.PlayVFX(KhosaryCode.VisualFeedbacks.VFXType.GunFire, _firePoint.position);
            }
        }
    }
}
