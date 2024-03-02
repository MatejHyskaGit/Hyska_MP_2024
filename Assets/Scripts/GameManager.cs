using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public string lastSceneName;
    public string pos = "";
    public Animator transition;
    public float transitionTime = 1f;
    GameObject light2D;

    public bool loading;

    public List<bool> Saves = new() { false, false, true, false };


    void Awake()
    {
        if (instance != null) Debug.LogWarning("More than one Game Manager in the scene");
        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            light2D = GameObject.Find("Global Light 2D");
            //light2D.SetActive(false);
        }
        else light2D.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }
}