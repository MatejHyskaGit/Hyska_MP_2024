using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        soundEffectsSource.volume = (float)GameManager.instance.VolumeSFX / 10;
        backgroundMusicSource.volume = (float)GameManager.instance.VolumeMusic / 10;
    }

    [Header("Sound Players")]
    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioSource backgroundMusicSource;

    [Header("Global")]
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip lockedDoor;
    [SerializeField] private AudioClip itemGet;
    [SerializeField] private AudioClip lifeLose;

    [Header("Menu")]
    [SerializeField] private AudioClip buttonSelectSound;
    [SerializeField] private AudioClip buttonPressSound;
    [SerializeField] private AudioClip menuMusic;

    [Header("Tavern")]
    [SerializeField] private AudioClip tavernMusic;


    [SerializeField] private AudioClip tavernM1Transition;


    [Header("MalirRoom3")]
    [SerializeField] private AudioClip basementMusic;
    [SerializeField] private AudioClip puzzleFinished;
    [SerializeField] private AudioClip fallHole;

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "openDoor": soundEffectsSource.clip = openDoor; break;
            case "lockedDoor": soundEffectsSource.clip = lockedDoor; break;
            case "itemGet": soundEffectsSource.clip = itemGet; break;
            case "buttonSelectSound": soundEffectsSource.clip = buttonSelectSound; break;
            case "buttonPressSound": soundEffectsSource.clip = buttonPressSound; break;
            case "puzzleFinished": soundEffectsSource.clip = puzzleFinished; break;
            case "fallHole": soundEffectsSource.clip = fallHole; break;
            default: return;
        }
        soundEffectsSource.Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "menuMusic": backgroundMusicSource.clip = menuMusic; break;
            case "tavernMusic": backgroundMusicSource.clip = tavernMusic; break;
            case "basementMusic": backgroundMusicSource.clip = basementMusic; break;
            default: return;
        }
        backgroundMusicSource.Play();
    }

}
