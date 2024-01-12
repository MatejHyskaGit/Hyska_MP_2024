using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhenInteracted : MonoBehaviour
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

    public void Interact(string name)
    {
        Scene scene = SceneManager.GetActiveScene();


        if (scene.name == "Room1")
        {
            if (name == "DoorToNext") script.LoadScene("MalirRoom1", "up");
            if (name == "DoorToNext1") script.LoadScene("MalirRoom1", "down");
            if (name == "DoorBack" || name == "DoorBack1") script.LoadScene("SampleScene");
        }
        if (scene.name == "SampleScene")
        {
            if (name == "DoorToNext" || name == "DoorToNext1") script.LoadScene("Room1");
        }
        /*
        Debug.Log("You interacted with" + name);
        if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("Room1");
        if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("SampleScene");*/
    }
}
