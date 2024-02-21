using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Indestructible : MonoBehaviour
{
    public string lastSceneName;
    public string pos = "";
    public Animator transition;
    public float transitionTime = 1f;
    GridMovement MovementScript;
    GameObject light2D;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        try
        {
            MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        }
        catch (System.Exception)
        {
        }
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            light2D = GameObject.Find("Global Light 2D");
            //light2D.SetActive(false);
        }
        else light2D.SetActive(true);
        if (MovementScript != null) MovementScript.movementAllowed = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        MovementScript.movementAllowed = true;
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            light2D.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadLevel(sceneName));
    }
    public void LoadScene(string sceneName, string position)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        pos = position;
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadLevel(sceneName));
        //Debug.Log("Happem");
        //MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        //MovementScript.movementAllowed = true;
    }


    IEnumerator LoadLevel(string sceneName)
    {
        if (MovementScript != null) MovementScript.movementAllowed = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }
}
