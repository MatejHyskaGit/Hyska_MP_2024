using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInitializer : MonoBehaviour
{
    [Header("Tavern")]
    [SerializeField] public GameObject[] tavernNPCList;

    [SerializeField] private GameObject Nathaniel;
    [SerializeField] private GameObject NathanielTable;
    [SerializeField] private GameObject NathanielNPC;

    [Header("MalirRoom1")]
    [SerializeField] public GameObject[] MRoom1NPCList;

    [Header("MalirRoom3")]
    [SerializeField] public GameObject[] MRoom3NPCList;

    public static RoomInitializer instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public bool CanExitTavern()
    {
        foreach (GameObject obj in tavernNPCList)
        {
            if (!GameManager.instance.dialogueIndex.ContainsKey(obj.name)) return false;
        }
        return true;
    }

    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "Tavern")
        {
            Debug.Log(GameManager.instance.dialogueIndex);
            foreach (GameObject obj in tavernNPCList)
            {
                if (GameManager.instance.dialogueIndex.ContainsKey(obj.name))
                {
                    Debug.Log(obj.name);
                    Debug.Log(GameManager.instance.dialogueIndex[obj.name]);
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = GameManager.instance.dialogueIndex[obj.name];
                }
                else
                {
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = 0;
                }
            }
            if (GameManager.instance.NathanielGone)
            {
                Nathaniel.SetActive(false);
                NathanielTable.SetActive(true);
                NathanielNPC.SetActive(false);
            }
            else
            {
                Nathaniel.SetActive(true);
                NathanielTable.SetActive(false);
                NathanielNPC.SetActive(true);
            }
        }
        if (SceneManager.GetActiveScene().name == "MalirRoom1")
        {
            foreach (GameObject obj in MRoom1NPCList)
            {
                if (GameManager.instance.dialogueIndex.ContainsKey(obj.name))
                {
                    Debug.Log(obj.name);
                    Debug.Log(GameManager.instance.dialogueIndex[obj.name]);
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = GameManager.instance.dialogueIndex[obj.name];
                }
                else
                {
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = 0;
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "MalirRoom3")
        {
            foreach (GameObject obj in MRoom3NPCList)
            {
                if (GameManager.instance.dialogueIndex.ContainsKey(obj.name))
                {
                    Debug.Log(obj.name);
                    Debug.Log(GameManager.instance.dialogueIndex[obj.name]);
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = GameManager.instance.dialogueIndex[obj.name];
                }
                else
                {
                    obj.GetComponentInChildren<DialogueTrigger>().dialogueIndex = 0;
                }
            }
        }
    }
}
