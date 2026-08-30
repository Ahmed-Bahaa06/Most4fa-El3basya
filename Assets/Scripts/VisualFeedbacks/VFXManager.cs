using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KhosaryCode.VisualFeedbacks
{
    [System.Serializable]
    public class VFXPoolData
    {
        public VFXType Type;
        public ParticleSystem Prefab;
        public int PoolSize = 5;
        
        [HideInInspector] public List<ParticleSystem> Pool = new List<ParticleSystem>();
    }

    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance { get; private set; }

        [Header("VFX Pools Setup")]
        [SerializeField] private List<VFXPoolData> _vfxPools = new List<VFXPoolData>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var poolData in _vfxPools)
            {
                if (poolData.Prefab == null) continue;

                // Create a parent container for neatness
                GameObject container = new GameObject($"Pool_{poolData.Type}");
                container.transform.SetParent(transform);

                for (int i = 0; i < poolData.PoolSize; i++)
                {
                    ParticleSystem vfx = Instantiate(poolData.Prefab, container.transform);
                    vfx.gameObject.SetActive(false);
                    poolData.Pool.Add(vfx);
                }
            }
        }

        /// <summary>
        /// Plays a VFX at the given position.
        /// </summary>
        public void PlayVFX(VFXType type, Vector3 position, Quaternion rotation = default)
        {
            VFXPoolData data = _vfxPools.Find(p => p.Type == type);
            if (data == null || data.Prefab == null) 
            {
                // Commented out to prevent console spam if VFX aren't assigned yet
                // Debug.LogWarning($"VFXManager: No prefab assigned for {type}");
                return;
            }

            ParticleSystem vfxToPlay = GetAvailableVFX(data);
            if (vfxToPlay != null)
            {
                vfxToPlay.transform.position = position;
                if (rotation != default)
                {
                    vfxToPlay.transform.rotation = rotation;
                }
                
                vfxToPlay.gameObject.SetActive(true);
                vfxToPlay.Play();
                
                StartCoroutine(ReturnToPoolRoutine(vfxToPlay));
            }
        }

        private ParticleSystem GetAvailableVFX(VFXPoolData data)
        {
            // Find inactive
            for (int i = 0; i < data.Pool.Count; i++)
            {
                if (!data.Pool[i].gameObject.activeInHierarchy)
                {
                    return data.Pool[i];
                }
            }

            // Expand pool if necessary
            GameObject container = transform.Find($"Pool_{data.Type}")?.gameObject;
            if (container == null)
            {
                container = new GameObject($"Pool_{data.Type}");
                container.transform.SetParent(transform);
            }

            ParticleSystem newVfx = Instantiate(data.Prefab, container.transform);
            newVfx.gameObject.SetActive(false);
            data.Pool.Add(newVfx);
            return newVfx;
        }

        private IEnumerator ReturnToPoolRoutine(ParticleSystem vfx)
        {
            if (vfx == null) yield break;
            
            // Calculate duration before waiting, so we don't access it later if it gets destroyed!
            float waitTime = vfx.main.duration + vfx.main.startLifetime.constantMax;
            GameObject vfxObj = vfx.gameObject;
            
            yield return new WaitForSeconds(waitTime);
            
            if (vfxObj != null)
            {
                vfxObj.SetActive(false);
            }
        }
    }
}
