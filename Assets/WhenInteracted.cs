using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhenInteracted : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if (scene.name == "MalirRoom1")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("MalirRoom2");
            if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("Room1");
        }
        if (scene.name == "Room1")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("MalirRoom1");
            if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("SampleScene");
        }
        if (scene.name == "SampleScene")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("Room1");
        }
        /*
        Debug.Log("You interacted with" + name);
        if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("Room1");
        if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("SampleScene");*/
    }
}
