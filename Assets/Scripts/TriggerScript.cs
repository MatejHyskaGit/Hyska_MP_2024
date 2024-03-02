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
            if (name == "DoorBack") GameManager.instance.LoadScene("Room1", "up");
            if (name == "DoorBack1") GameManager.instance.LoadScene("Room1", "down");
        }
        if (scene.name == "MalirRoom2")
        {
            if (name == "DiraTrigger" || name == "DiraTrigger1") GameManager.instance.LoadScene("MalirRoom3");
            if (name == "DoorBack") GameManager.instance.LoadScene("MalirRoom1", "up");
            if (name == "DoorBack1") GameManager.instance.LoadScene("MalirRoom1", "down");
        }

        /*
        Debug.Log("You triggered" + name);
        if(name == "trigger") SceneManager.LoadScene("Room1");*/
    }
}