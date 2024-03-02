using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhenInteracted : MonoBehaviour
{
    public void Interact(string name)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MalirRoom1")
        {

        }
        if (scene.name == "Room1")
        {
            if (name == "DoorToNext") GameManager.instance.LoadScene("MalirRoom1", "up");
            if (name == "DoorToNext1") GameManager.instance.LoadScene("MalirRoom1", "down");
            if (name == "DoorBack" || name == "DoorBack1") GameManager.instance.LoadScene("SampleScene");
        }
        if (scene.name == "SampleScene")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") GameManager.instance.LoadScene("Room1");
        }
        /*
        Debug.Log("You interacted with" + name);
        if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("Room1");
        if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("SampleScene");*/
    }
}