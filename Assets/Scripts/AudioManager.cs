using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

}
