using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace KhosaryCode.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [Header("Audio Source Pool")]
        [SerializeField] private int _poolSize = 15;
        [SerializeField] private GameObject _audioSourcePrefab;
        private List<AudioSource> _pool = new List<AudioSource>();

        [Header("Player Sounds")]
        [SerializeField] private SoundGroup _playerFootstep = new SoundGroup(1f);
        [SerializeField] private SoundGroup _playerKnockoutHit = new SoundGroup(1f);
        [SerializeField] private SoundGroup _playerHurt = new SoundGroup(1f);
        [SerializeField] private SoundGroup _playerDie = new SoundGroup(1f);

        [Header("NPC Sounds (Movement)")]
        [SerializeField] private SoundGroup _npcIdleStep = new SoundGroup(0.7f);
        [SerializeField] private SoundGroup _npcRunStep = new SoundGroup(1f);

        [Header("NPC Sounds (Male Voices)")]
        [SerializeField] private SoundGroup _maleNpcHurt = new SoundGroup(1f);
        [SerializeField] private SoundGroup _maleNpcDie = new SoundGroup(1f);
        [SerializeField] private SoundGroup _doctorSpotPlayer = new SoundGroup(1f);
        [SerializeField] private SoundGroup _guardMeleeSpotPlayer = new SoundGroup(1f);
        [SerializeField] private SoundGroup _guardRangedSpotPlayer = new SoundGroup(1f);

        [Header("NPC Sounds (Female Voices)")]
        [SerializeField] private SoundGroup _femaleNpcHurt = new SoundGroup(1f);
        [SerializeField] private SoundGroup _femaleNpcDie = new SoundGroup(1f);
        [SerializeField] private SoundGroup _femaleDoctorSpotPlayer = new SoundGroup(1f);

        [Header("UI & VFX Sounds")]
        [SerializeField] private SoundGroup _uiButtonClick = new SoundGroup(1f);
        [SerializeField] private SoundGroup _uiButtonHover = new SoundGroup(1f);
        [SerializeField] private SoundGroup _vfxSounds = new SoundGroup(1f);

        [Header("BackgroundSounds")]
        [SerializeField] private SoundGroup _ambientSound =  new SoundGroup(1f);
         [SerializeField] private SoundGroup _flickerSound = new SoundGroup(1f);

        [Header("Background Audio (Loops)")]
        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _flickerSource;
        [SerializeField] private string _cutsceneSceneName = "Cutscene";

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

        private void Start()
        {
            // Load saved volumes (default to 1f if they haven't been saved yet)
            SetBackgroundVolume(PlayerPrefs.GetFloat("BGM_Volume", 1f));
            SetSFXVolume(PlayerPrefs.GetFloat("SFX_Volume", 1f));
        }

        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            bool isCutscene = scene.name.Contains(_cutsceneSceneName, System.StringComparison.OrdinalIgnoreCase);

            if (isCutscene)
            {
                if (_ambientSource != null) _ambientSource.Pause();
                if (_flickerSource != null) _flickerSource.Pause();
            }
            else
            {
                if (_ambientSource != null && !_ambientSource.isPlaying) _ambientSource.Play();
                if (_flickerSource != null && !_flickerSource.isPlaying) _flickerSource.Play();
            }
        }

        [Header("Volume Control")]
        [Range(0.0001f, 1f)] public float BackgroundVolume = 1f;
        [Range(0.0001f, 1f)] public float SFXVolume = 1f;

        public void SetBackgroundVolume(float volume)
        {
            BackgroundVolume = Mathf.Clamp01(volume);
            if (_ambientSource != null) _ambientSource.volume = BackgroundVolume;
            if (_flickerSource != null) _flickerSource.volume = BackgroundVolume;
            
            PlayerPrefs.SetFloat("BGM_Volume", BackgroundVolume);
            PlayerPrefs.Save();
        }

        public void SetSFXVolume(float volume)
        {
            SFXVolume = Mathf.Clamp01(volume);
            
            PlayerPrefs.SetFloat("SFX_Volume", SFXVolume);
            PlayerPrefs.Save();
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
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeInHierarchy)
                {
                    return _pool[i];
                }
            }

            GameObject obj = Instantiate(_audioSourcePrefab, transform);
            AudioSource newSource = obj.GetComponent<AudioSource>();
            newSource.playOnAwake = false;
            _pool.Add(newSource);
            return newSource;
        }

        /// <summary>
        /// Plays a sound from the specified category at the given world position.
        /// You can pass a maxDuration to force the sound to stop early (e.g. cutting a 6-second footstep clip down to 0.3s).
        /// </summary>
        public void PlaySound(SoundType type, Vector3 position = default, float maxDuration = -1f)
        {
            SoundGroup group = GetSoundGroup(type);
            AudioClip clip = group.GetRandomClip();

            if (clip == null) return;

            AudioSource source = GetAvailableSource();
            source.gameObject.SetActive(true);
            source.transform.position = position;
            
            source.clip = clip;
            source.volume = group.Volume * SFXVolume;
            source.pitch = Random.Range(group.PitchRange.x, group.PitchRange.y);
            
            if (type == SoundType.UIButtonClick || type == SoundType.UIButtonHover)
            {
                source.spatialBlend = 0f; // 2D sound for UI
            }
            else
            {
                source.spatialBlend = 1f; // 3D sound for everything else
            }

            source.Play();

            float playTime = (maxDuration > 0f && maxDuration < clip.length) ? maxDuration : clip.length;
            StartCoroutine(ReturnToPoolRoutine(source, playTime));
        }

        private IEnumerator ReturnToPoolRoutine(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (source.isPlaying) source.Stop();
            source.gameObject.SetActive(false);
        }

        private SoundGroup GetSoundGroup(SoundType type)
        {
            switch (type)
            {
                case SoundType.PlayerFootstep: return _playerFootstep;
                case SoundType.PlayerKnockoutHit: return _playerKnockoutHit;
                case SoundType.PlayerHurt: return _playerHurt;
                case SoundType.PlayerDie: return _playerDie;

                case SoundType.NPCIdleStep: return _npcIdleStep;
                case SoundType.NPCRunStep: return _npcRunStep;
                
                case SoundType.MaleNPCHurt: return _maleNpcHurt;
                case SoundType.MaleNPCDie: return _maleNpcDie;
                case SoundType.DoctorSpotPlayer: return _doctorSpotPlayer;
                case SoundType.GuardMeleeSpotPlayer: return _guardMeleeSpotPlayer;
                case SoundType.GuardRangedSpotPlayer: return _guardRangedSpotPlayer;

                case SoundType.FemaleNPCHurt: return _femaleNpcHurt;
                case SoundType.FemaleNPCDie: return _femaleNpcDie;
                case SoundType.FemaleDoctorSpotPlayer: return _femaleDoctorSpotPlayer;

                case SoundType.UIButtonClick: return _uiButtonClick;
                case SoundType.UIButtonHover: return _uiButtonHover;
                case SoundType.VFX: return _vfxSounds;
                
                default: return default;
            }
        }
    }
}
