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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        Debug.Log("Hybaj");
        MovementScript.movementAllowed = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        MovementScript = GameObject.Find("Player").GetComponent<GridMovement>();
        MovementScript.movementAllowed = true;
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
        Debug.Log("Štop");
        MovementScript.movementAllowed = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }
}
