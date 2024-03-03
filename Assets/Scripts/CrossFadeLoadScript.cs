using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFadeLoadScript : MonoBehaviour
{
    public void SetLoadTrue()
    {
        GameManager.instance.loading = true;
    }
    public void SetLoadFalse()
    {
        GameManager.instance.loading = false;
        if (SceneManager.GetActiveScene().name == "MalirRoom3")
        {
            GameManager.instance.loading = true;
            SchuteFallManager.instance.StartChute();
        }
        if (SceneManager.GetActiveScene().name == "Tavern")
        {
            GameObject.Find("InitDialogue").GetComponentInChildren<DialogueTrigger>().StartInit();
        }
    }
}