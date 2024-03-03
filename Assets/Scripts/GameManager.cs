using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public string lastSceneName;
    public string pos = "";
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private GameObject light2D;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu": light2D.GetComponent<Light2D>().intensity = 0.035f; break;
            case "MalirRoom1": light2D.GetComponent<Light2D>().intensity = 0.4f; break;
            case "MalirRoom3": light2D.GetComponent<Light2D>().intensity = 0f; break;
            default: light2D.GetComponent<Light2D>().intensity = 0.1f; break;
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