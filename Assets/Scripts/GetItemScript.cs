using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetItemScript : MonoBehaviour
{
    public void SetGetTrue()
    {
        Time.timeScale = 0f;
        GameManager.instance.gettingItem = true;
    }
    public void SetGetFalse()
    {
        Time.timeScale = 1f;
        GameManager.instance.gettingItem = false;
    }
}