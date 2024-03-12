using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhenInteracted : MonoBehaviour
{
    public void Interact(string name)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Tavern")
        {
            if (name == "DoorToNext")
            {
                if (RoomInitializer.instance.CanExitTavern())
                {
                    if (GameManager.instance.NathanielGone)
                    {
                        GameManager.instance.LoadScene("MalirRoom1");
                    }
                    else
                    {
                        AnimationDialogue.Instance.TavernAnimationOne();
                    }
                    //start animation
                }
                else
                {
                    TextAsset noexitdialogue = Resources.Load<TextAsset>("Dialogues/NoTavernExit");
                    DialogueManager.instance.EnterDialogueMode(noexitdialogue);
                }
            }

        }
        if (scene.name == "MalirRoom5")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") GameManager.instance.LoadScene("MalirRoom6");
            if (name == "DoorBack" || name == "DoorBack1") GameManager.instance.LoadScene("MalirRoom4");
        }
    }
}