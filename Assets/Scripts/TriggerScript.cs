using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    public void Trigger(string name)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MalirRoom1")
        {
            if (name == "DoorToNext") GameManager.instance.LoadScene("MalirRoom2", "up");
            if (name == "DoorToNext1") GameManager.instance.LoadScene("MalirRoom2", "down");
            if (name == "DoorBack") GameManager.instance.LoadScene("Tavern");
            if (name == "DoorBack1") GameManager.instance.LoadScene("Tavern");
        }
        if (scene.name == "MalirRoom2")
        {
            if (name == "DiraTrigger" || name == "DiraTrigger1") GameManager.instance.LoadScene("MalirRoom3");
            if (name == "DoorBack") GameManager.instance.LoadScene("MalirRoom1", "up");
            if (name == "DoorBack1") GameManager.instance.LoadScene("MalirRoom1", "down");
        }
        if (scene.name == "MalirRoom3")
        {
            if (name == "DoorToNext")
            {
                if (!StatueManager.isLocked) GameManager.instance.LoadScene("MalirRoom4", "up");
                else
                {
                    TextAsset lockedDialogue = Resources.Load<TextAsset>("Dialogues/LockedDialogue");
                    DialogueManager.instance.EnterDialogueMode(lockedDialogue);
                }
            }

            if (name == "DoorToNext1")
            {
                if (!StatueManager.isLocked) GameManager.instance.LoadScene("MalirRoom4", "down");
                else
                {
                    TextAsset lockedDialogue = Resources.Load<TextAsset>("Dialogues/LockedDialogue");
                    DialogueManager.instance.EnterDialogueMode(lockedDialogue);
                }
            }
        }
        if (scene.name == "MalirRoom4")
        {
            if (name == "DoorToNext") GameManager.instance.LoadScene("MalirRoom5");
            if (name == "DoorBack") GameManager.instance.LoadScene("MalirRoom3", "up");
            if (name == "DoorBack1") GameManager.instance.LoadScene("MalirRoom3", "down");
        }
        if (scene.name == "MalirRoom5")
        {
            TextAsset dontFallDialogue = Resources.Load<TextAsset>("Dialogues/DontFallDialogue");
            if (name == "trigger" || name == "trigger1") DialogueManager.instance.EnterDialogueMode(dontFallDialogue);
        }

        /*
        Debug.Log("You triggered" + name);
        if(name == "trigger") SceneManager.LoadScene("Room1");*/
    }
}