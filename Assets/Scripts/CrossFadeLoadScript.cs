using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFadeLoadScript : MonoBehaviour
{

    public static void SceneUpdate()
    {
        if (SceneManager.GetActiveScene().name == "MalirRoom3" && GameManager.instance.lastSceneName == "MalirRoom2")
        {
            SchuteFallManager.instance.CameraVC.Follow = null;
        }
        if (SceneManager.GetActiveScene().name == "MalirRoom3" && GameManager.instance.lastSceneName == "MalirRoom4")
        {
            SchuteFallManager.instance.CameraVC.Follow = SchuteFallManager.instance.PlayerObject.transform;
        }
    }
    public void SetLoadTrue()
    {
        GameManager.instance.loading = true;
        if (SceneManager.GetActiveScene().name == "MalirRoom3" && GameManager.instance.lastSceneName == "Menu")
        {
            GameManager.instance.loading = true;
            SchuteFallManager.instance.StartChute();
        }

    }
    public void SetLoadFalse()
    {
        GameManager.instance.loading = false;
        if (SceneManager.GetActiveScene().name == "MalirRoom3" && (GameManager.instance.lastSceneName == "MalirRoom2" || GameManager.instance.lastSceneName == "Menu"))
        {
            GameManager.instance.loading = true;
            SchuteFallManager.instance.StartChute();
        }
        if (SceneManager.GetActiveScene().name == "Tavern")
        {
            GameObject.Find("InitDialogue").GetComponentInChildren<DialogueTrigger>().StartInit();
        }
        if (SceneManager.GetActiveScene().name == "MalirRoom6")
        {
            GameObject.Find("InitDialogueM6").GetComponentInChildren<DialogueTrigger>().StartInit();
        }
    }
}