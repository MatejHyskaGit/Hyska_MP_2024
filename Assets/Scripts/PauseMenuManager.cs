using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;

    [SerializeField] GameObject[] pauseButtonObjects;

    private Button[] pauseButtons;

    public static bool isPaused { get; private set; }

    private int selectedIndex = 0;

    void Start()
    {
        pauseButtons = new Button[pauseButtonObjects.Length];
        PausePanel.SetActive(false);

        int index = 0;
        foreach (GameObject obj in pauseButtonObjects)
        {
            pauseButtons[index] = obj.GetComponent<Button>();
            index++;
        }
    }

    void Update()
    {
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                HandleButtonMove(KeyCode.RightArrow);
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                HandleButtonMove(KeyCode.LeftArrow);
                return;
            }
            //Button Handle Logic
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Options()
    {
        throw new NotImplementedException();
    }

    public void MainMenu()
    {
        Resume();
        GameManager.instance.LoadScene("Menu");
    }


    private void HandleButtonMove(KeyCode pressed)
    {
        if (pressed == KeyCode.RightArrow)
        {
            if (pauseButtons[selectedIndex + 1].IsActive())
            {
                selectedIndex++;
            }
        }
        if (pressed == KeyCode.LeftArrow)
        {
            if (selectedIndex != 0)
            {
                selectedIndex--;
            }
        }
    }
}
