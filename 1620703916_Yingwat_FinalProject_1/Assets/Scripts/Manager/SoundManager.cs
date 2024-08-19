using System;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundClip[] soundClips;

        public static SoundManager Instance { get; private set; }


        public void Start()
        {
            Play(SoundManager.Sound.BGM);
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);                
            }
        }

        public enum Sound
        {
            BGM,
            PlayerFire,
            PlayerExploded,
            EnemyFire,
            EnemyExploded,
            BossFire,
            BossExploded
        }
        
        [Serializable] 
        private class SoundClip
        {
            public Sound Sound;
            public AudioClip audioClip;
            [Range(0,1)]public float soundVolume;
            public bool loop;
            [HideInInspector]public AudioSource audioSource;
        }
        public void Play(Sound sound)
        {
            var soundClip = GetSoundClip(sound);
            if (soundClip.audioSource == null)
            {   
                soundClip.audioSource = gameObject.AddComponent<AudioSource>();
            }
            soundClip.audioSource.clip = soundClip.audioClip;
            soundClip.audioSource.volume = soundClip.soundVolume;
            soundClip.audioSource.loop = soundClip.loop;
            soundClip.audioSource.Play();
        }

        private SoundClip GetSoundClip(Sound sound)
        {
            foreach (var soundClip in soundClips)
            {
                if (soundClip.Sound == sound)
                {
                    return soundClip;
                }
            }
            return null;
        }
    }

}

