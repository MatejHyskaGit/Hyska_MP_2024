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
        Debug.Log("You interacted with" + name);
        if (name == "DoorToNext" || name == "DoorToNext1") SceneManager.LoadScene("Room1");
        if (name == "DoorBack" || name == "DoorBack1") SceneManager.LoadScene("SampleScene");
    }
}
