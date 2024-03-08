using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions : MonoBehaviour
{
    public void Bind(Story story, GameObject obj, GameObject obj2)
    {
        story.BindExternalFunction("startDice", () =>
        {
            obj.SetActive(true);
        });
        story.BindExternalFunction("startNextDialogue", () =>
        {
            obj2.GetComponentInChildren<DialogueTrigger>().EnterDialogue();
        });
    }


    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startDice");
        story.UnbindExternalFunction("startNextDialogue");
    }
}
