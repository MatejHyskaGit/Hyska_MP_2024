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
            if (name == "DoorToNext") GameManager.instance.LoadScene("MalirRoom1");
        }
    }
}