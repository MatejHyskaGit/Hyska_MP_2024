using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        soundEffectsSource.volume = (float)(GameManager.instance.Volume / 10);
        backgroundMusicSource.volume = (float)(GameManager.instance.Volume / 10);
    }

    [Header("Sound Players")]
    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioSource backgroundMusicSource;

    [Header("Global")]
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip lockedDoor;
    [SerializeField] private AudioClip itemGet;

    [Header("Menu")]
    [SerializeField] private AudioClip buttonSelectSound;
    [SerializeField] private AudioClip buttonPressSound;

    [Header("Tavern")]
    [SerializeField] private AudioClip tavernMusic;


    [SerializeField] private AudioClip tavernM1Transition;


    [Header("MalirRoom3")]
    [SerializeField] private AudioClip basementMusic;
    [SerializeField] private AudioClip puzzleFinished;

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
            default: return;
        }
        soundEffectsSource.Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "tavernMusic": backgroundMusicSource.clip = tavernMusic; break;
            case "basementMusic": backgroundMusicSource.clip = basementMusic; break;
            default: return;
        }
        backgroundMusicSource.Play();
    }

}
