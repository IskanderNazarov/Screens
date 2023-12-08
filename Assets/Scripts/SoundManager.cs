using System;
using UnityEngine;

namespace DefaultNamespace {
    public class SoundManager:MonoBehaviour {
        public static SoundManager shared { private set; get; }
        
        private const string IS_SOUND_ON_KEY = "sound_on_key";
        private const string IS_MUSIC_ON_KEY = "music_on_key";

        private void Awake() {
            if (shared == null) {
                shared = this;
            } else if (shared != this) {
                Destroy(gameObject);
            }
        }

        [SerializeField] private AudioSource bgMusicAudio;
        [SerializeField] private AudioClip bgMusic;
        public bool IsSoundOn { get; private set; }
        public bool IsMusicOn { get; private set; }
        private float bgMusicOriginVolume = 0.5f;

        private void Start() {
            IsSoundOn = PlayerPrefs.GetInt(IS_SOUND_ON_KEY, 1) == 1;
            IsMusicOn = PlayerPrefs.GetInt(IS_MUSIC_ON_KEY, 1) == 1;

            bgMusicAudio.volume = bgMusicOriginVolume;
            bgMusicAudio.clip = bgMusic;
            bgMusicAudio.Play();
        }

        private void UpdateBgMusicState() {
            bgMusicAudio.volume = IsMusicOn ? bgMusicOriginVolume : 0;
        }

        public void PlaySound(AudioSource source, AudioClip clip, float volume = 1) {
            if (!IsSoundOn || source == null) return;
            
            source.clip = clip;
            source.volume = volume;
            source.Play();
        }

        public void ChangeMusicOnOffState() {
            IsMusicOn = !IsMusicOn;
            PlayerPrefs.SetInt(IS_MUSIC_ON_KEY, IsMusicOn ? 1 : 0);
            UpdateBgMusicState();
        }
        
        public void ChangeSoundOnOffState() {
            IsSoundOn = !IsSoundOn;
            PlayerPrefs.SetInt(IS_SOUND_ON_KEY, IsSoundOn ? 1 : 0);
        }
    }
}