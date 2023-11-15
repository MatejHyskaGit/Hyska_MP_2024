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
        Debug.Log("You triggered" + name);
        if(name == "trigger") SceneManager.LoadScene("Room1");
    }
}
