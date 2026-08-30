using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace KhosaryCode.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [Header("Audio Source Pool")]
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private GameObject _audioSourcePrefab;
        private List<AudioSource> _pool = new List<AudioSource>();

        [Header("Player Sounds")]
        [SerializeField] private SoundGroup _playerAction = new SoundGroup(1f);
        [SerializeField] private SoundGroup _playerKnockdown = new SoundGroup(1f);

        [Header("Guard Sounds")]
        [SerializeField] private SoundGroup _guardMeleeSpot = new SoundGroup(1f);
        [SerializeField] private SoundGroup _guardRangedSpot = new SoundGroup(1f);

        [Header("Doctor Sounds")]
        [SerializeField] private SoundGroup _doctorSpot = new SoundGroup(1f);
        [SerializeField] private SoundGroup _femaleDoctorSpot = new SoundGroup(1f);

        [Header("Other Sounds")]
        [SerializeField] private SoundGroup _uiSounds = new SoundGroup(1f);
        [SerializeField] private SoundGroup _vfxSounds = new SoundGroup(1f);

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializePool();
        }

        private void InitializePool()
        {
            if (_audioSourcePrefab == null)
            {
                // Fallback if no prefab is assigned
                _audioSourcePrefab = new GameObject("PooledAudioSource");
                _audioSourcePrefab.AddComponent<AudioSource>();
                _audioSourcePrefab.transform.SetParent(transform);
            }

            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = Instantiate(_audioSourcePrefab, transform);
                AudioSource source = obj.GetComponent<AudioSource>();
                source.playOnAwake = false;
                obj.SetActive(false);
                _pool.Add(source);
            }
        }

        private AudioSource GetAvailableSource()
        {
            // Find an inactive source in the pool
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeInHierarchy)
                {
                    return _pool[i];
                }
            }

            // Expand pool if necessary
            GameObject obj = Instantiate(_audioSourcePrefab, transform);
            AudioSource newSource = obj.GetComponent<AudioSource>();
            newSource.playOnAwake = false;
            _pool.Add(newSource);
            return newSource;
        }

        /// <summary>
        /// Plays a sound from the specified category at the given world position.
        /// </summary>
        public void PlaySound(SoundType type, Vector3 position)
        {
            SoundGroup group = GetSoundGroup(type);
            AudioClip clip = group.GetRandomClip();

            if (clip == null) return;

            AudioSource source = GetAvailableSource();
            source.gameObject.SetActive(true);
            source.transform.position = position;
            
            source.clip = clip;
            source.volume = group.Volume;
            source.pitch = Random.Range(group.PitchRange.x, group.PitchRange.y);
            
            // For UI Sounds, we typically don't want 3D spatialization.
            // If the prefab has spatialBlend = 1, we can override it for UI.
            if (type == SoundType.UI)
            {
                source.spatialBlend = 0f;
            }
            else
            {
                // Assuming standard 3D sound is set in the prefab, but let's enforce it
                source.spatialBlend = 1f;
            }

            source.Play();

            // Return to pool after playing
            StartCoroutine(ReturnToPoolRoutine(source, clip.length));
        }

        private IEnumerator ReturnToPoolRoutine(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            source.gameObject.SetActive(false);
        }

        private SoundGroup GetSoundGroup(SoundType type)
        {
            switch (type)
            {
                case SoundType.PlayerAction: return _playerAction;
                case SoundType.PlayerKnockdown: return _playerKnockdown;
                case SoundType.GuardMeleeSpot: return _guardMeleeSpot;
                case SoundType.GuardRangedSpot: return _guardRangedSpot;
                case SoundType.DoctorSpot: return _doctorSpot;
                case SoundType.FemaleDoctorSpot: return _femaleDoctorSpot;
                case SoundType.UI: return _uiSounds;
                case SoundType.VFX: return _vfxSounds;
                default: return default;
            }
        }
    }
}
