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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        lastSceneName = SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(lastSceneName);
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(string sceneName, string position)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        pos = position;
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadLevel(sceneName));
    }


    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }
}
