using UnityEngine;

namespace KhosaryCode.Audio
{
    [System.Serializable]
    public struct SoundGroup
    {
        public AudioClip[] Clips;
        
        [Range(0f, 1f)]
        public float Volume;
        
        public Vector2 PitchRange;

        public SoundGroup(float defaultVolume = 1f)
        {
            Clips = new AudioClip[0];
            Volume = defaultVolume;
            PitchRange = new Vector2(0.95f, 1.05f);
        }
        
        public AudioClip GetRandomClip()
        {
            if (Clips == null || Clips.Length == 0)
                return null;
                
            int index = Random.Range(0, Clips.Length);
            return Clips[index];
        }
    }
}
