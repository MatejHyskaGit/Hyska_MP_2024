using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationDialogue : MonoBehaviour
{

    public static AnimationDialogue Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void TavernAnimationOne()
    {
        GameManager.instance.loading = true;
        TextAsset TavernExit1 = Resources.Load<TextAsset>("Dialogues/TavernExit1");
        DialogueManager.instance.EnterDialogueMode(TavernExit1);
        //pustíš dialog 1 → animace přiběhne Nathaniel → zbytek dialogu
    }
    public void TavernAnimationTwo()
    {
        MovementManager.instance.Direction = Direction.right;
        MovementManager.instance.animator.SetFloat("Direction", (float)Direction.right);
        //zbytek dialogu
        TextAsset TavernExit2 = Resources.Load<TextAsset>("Dialogues/TavernExit2");
        DialogueManager.instance.EnterDialogueMode(TavernExit2);

        //na konci nathaniel vleze do tebe
        //na konci animace loading false
    }
    public void TavernAnimationEnd()
    {
        GameManager.instance.NathanielGone = true;

        GameManager.instance.loading = false;

        GameObject.Find("NPCNathaniel").transform.GetChild(1).gameObject.SetActive(false);
    }
}
