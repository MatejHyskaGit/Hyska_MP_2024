using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Trigger(string name)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MalirRoom2")
        {
            if (name == "DiraTrigger" || name == "DiraTrigger1") SceneManager.LoadScene("MalirRoom3");
            if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("MalirRoom1");
        }
        /*
        Debug.Log("You triggered" + name);
        if(name == "trigger") SceneManager.LoadScene("Room1");*/
    }
}
