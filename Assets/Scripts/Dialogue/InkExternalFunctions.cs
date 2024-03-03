using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story, GameObject DiePopup)
    {
        story.BindExternalFunction("startDice", () =>
        {
            DiePopup.SetActive(true);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("startDice");
    }
}
