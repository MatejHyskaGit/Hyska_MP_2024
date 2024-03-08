using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Main Screen")]
    [SerializeField] GameObject PauseCanvas;

    [SerializeField] GameObject PausePanel;

    [SerializeField] Animator pauseAnimator;

    [Header("Options Screen")]

    [SerializeField] GameObject SettingsPanel;
    [SerializeField] Animator settingsAnimator;
    [SerializeField] Image[] optionsSoundImages;
    [SerializeField] private Image VsyncImageSwitch;
    [SerializeField] private Sprite volumeSpriteOn;
    [SerializeField] private Sprite volumeSpriteOff;
    [SerializeField] private Sprite switchSpriteOn;
    [SerializeField] private Sprite switchSpriteOff;

    bool insideVolume;


    public static bool isPaused { get; private set; }

    private int selectedIndex = 0;

    int selectedInnerIndex = 0;

    bool runCustomUpdate = false;
    bool optionsUpdate = false;

    void Start()
    {
        SettingsPanel.SetActive(false);
        PauseCanvas.SetActive(false);

        foreach (Image soundimage in optionsSoundImages)
        {
            soundimage.sprite = volumeSpriteOff;
        }
        for (int i = 0; i < GameManager.instance.Volume; i++)
        {
            optionsSoundImages[i].sprite = volumeSpriteOn;
        }
        if (QualitySettings.vSyncCount == 0)
        {
            VsyncImageSwitch.sprite = switchSpriteOff;
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            VsyncImageSwitch.sprite = switchSpriteOn;
        }
    }

    void Update()
    {
        if (runCustomUpdate)
        {
            if (optionsUpdate) OptionsLoop();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (selectedIndex != 0)
                {
                    selectedIndex--;
                    pauseAnimator.Play(selectedIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (selectedIndex != 4)
                {
                    selectedIndex++;
                    pauseAnimator.Play(selectedIndex.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                return;
            }
            //Button Handle Logic
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (selectedIndex)
                {
                    case 0: Resume(); break;
                    case 1: break;
                    case 2: break;
                    case 3: Options(); break;
                    case 4: MainMenu(); break;
                    default: return;
                }
            }
        }

    }

    void Pause()
    {
        PauseCanvas.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        selectedIndex = 0;
        pauseAnimator.Play(selectedIndex.ToString());
    }

    void Resume()
    {
        PauseCanvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Save()
    {
        throw new NotImplementedException();
    }

    void Options()
    {
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);

        runCustomUpdate = true;
        optionsUpdate = true;

        selectedInnerIndex = 0;
        settingsAnimator.Play(selectedInnerIndex.ToString());
    }

    void OptionsLoop()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!insideVolume)
            {
                if (selectedInnerIndex != 2) selectedInnerIndex++;
                settingsAnimator.Play(selectedInnerIndex.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

            if (!insideVolume)
            {
                if (selectedInnerIndex != 0) selectedInnerIndex--;
                settingsAnimator.Play(selectedInnerIndex.ToString());
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (insideVolume)
            {
                if (GameManager.instance.Volume != 0) GameManager.instance.Volume--;
                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.Volume; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (insideVolume)
            {
                if (GameManager.instance.Volume != 10) GameManager.instance.Volume++;
                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.Volume; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (insideVolume)
            {
                settingsAnimator.speed = 1f;
                insideVolume = false;
                return;
            }


            switch (selectedInnerIndex)
            {
                case 0:
                    insideVolume = true;
                    settingsAnimator.speed = 0f;
                    break;
                case 1:
                    if (VsyncImageSwitch.sprite == switchSpriteOff)
                    {
                        VsyncImageSwitch.sprite = switchSpriteOn;
                        QualitySettings.vSyncCount = 1;
                    }
                    else if (VsyncImageSwitch.sprite == switchSpriteOn)
                    {
                        VsyncImageSwitch.sprite = switchSpriteOff;
                        QualitySettings.vSyncCount = 0;
                    }
                    break; //změň sprite, vypni vsync
                case 2:
                    SettingsPanel.SetActive(false);
                    PausePanel.SetActive(true);
                    runCustomUpdate = false;
                    optionsUpdate = false;
                    pauseAnimator.Play(selectedIndex.ToString());
                    return;
                default: return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (insideVolume)
            {
                settingsAnimator.speed = 1f;
                insideVolume = false;
                return;
            }
            else
            {
                SettingsPanel.SetActive(false);
                PausePanel.SetActive(true);
                runCustomUpdate = false;
                optionsUpdate = false;
                return;
            }
        }
    }

    void MainMenu()
    {
        Resume();
        GameManager.instance.LoadScene("Menu");
    }
}
