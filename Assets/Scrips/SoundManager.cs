using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public bool soundFXEnabled = true;
    public bool musicEnabled = true;

    public AudioSource musicAudioSource;

    public enum Sound
    {
        UI,
        Laser,
        EnemyLaser,
        EnemySpawn,
        EnemyDestroyed,
        HealthRing,
        AmmoRing,
        PowerUp,
        HomeMenu,
        StartLevel
    }

    [Serializable]
    public class Sounds
    {
        public Sound sound;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
    }

    public List<Sounds> soundList;
    private Dictionary<Sound, List<Sounds>> soundDictionary;

    private void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicAudioSource = GetComponent<AudioSource>();

        soundDictionary = new Dictionary<Sound, List<Sounds>>();

        foreach (Sounds sound in soundList)
        {
            if (!soundDictionary.ContainsKey(sound.sound))
                soundDictionary[sound.sound] = new List<Sounds>();

            soundDictionary[sound.sound].Add(sound);
        }
    }

    public void ToggleSoundFX()
    {
        soundFXEnabled = !soundFXEnabled; 
        SoundSpriteUpdater.Instance.UpdateSoundSprite(soundFXEnabled);
        PlaySound(Sound.UI);
    }

    public void ToggleMusic()
    {   
        musicEnabled = !musicEnabled;
        musicAudioSource.mute = !musicEnabled; 
        SoundSpriteUpdater.Instance.UpdateMusicSprite(musicEnabled);
        PlaySound(Sound.UI);
    }

    public void PlaySound(Sound sound)
    {   
        if (!soundFXEnabled) return;

        if (soundDictionary.ContainsKey(sound))
        {
            List<Sounds> sounds = soundDictionary[sound];

            if (sounds.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, sounds.Count);
                AudioClip randomClip = sounds[randomIndex].audioClip;
                float volume = sounds[randomIndex].volume;
                // Play the random AudioClip here
                PlayAudioClip(randomClip, volume);
            }
            else
            {
                Debug.LogWarning("No audio clips found for sound: " + sound);
            }
        }
        else
        {
            Debug.LogError("Sound not found: " + sound);
        }
    }

    public void PlayAudioClip(AudioClip audioClip, float volume)
    {
        // Create a new game object to play the audio clip
        GameObject audioObject = new GameObject("AudioObject");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = audioClip;

        audioSource.Play();

        Destroy(audioObject, audioClip.length);
    }
}
