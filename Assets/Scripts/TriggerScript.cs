using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    private Indestructible script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Hugo").GetComponent<Indestructible>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Trigger(string name)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MalirRoom1")
        {
            if (name == "DoorToNext") script.LoadScene("MalirRoom2", "up");
            if (name == "DoorToNext1") script.LoadScene("MalirRoom2", "down");
            if (name == "DoorBack") script.LoadScene("Room1", "up");
            if (name == "DoorBack1") script.LoadScene("Room1", "down");
        }
        if (scene.name == "MalirRoom2")
        {
            if (name == "DiraTrigger" || name == "DiraTrigger1") script.LoadScene("MalirRoom3");
            if (name == "DoorBack") script.LoadScene("MalirRoom1", "up");
            if (name == "DoorBack1") script.LoadScene("MalirRoom1", "down");
        }

        /*
        Debug.Log("You triggered" + name);
        if(name == "trigger") SceneManager.LoadScene("Room1");*/
    }
}
