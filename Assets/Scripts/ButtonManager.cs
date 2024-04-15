using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Left Side")]
    [SerializeField] private GameObject[] leftButtonObjects;
    [SerializeField] private Animator leftAnimator;

    private Button[] leftButtons;

    [Header("Right Side")]
    [SerializeField] private GameObject[] rightButtonObjects;
    [SerializeField] private Animator rightAnimator;

    private Button[] rightButtons;

    [Header("New Game/Load Game Block")]
    [SerializeField] private GameObject[] saveFilesObjects;
    private Image[] saveFileImages;
    [SerializeField] private Sprite[] coloredSaveSprites;
    [SerializeField] private TextMeshProUGUI[] TimePlayedTexts;
    [SerializeField] private GameObject saveFilePart;
    [SerializeField] private GameObject yesNoPart;
    private Animator saveFileAnimator;
    private Animator yesNoAnimator;
    [SerializeField] private GameObject newGameTextObject;
    int leftFrom = 0;

    [Header("Options Block")]
    [SerializeField] private GameObject[] optionsSFXObjects;
    [SerializeField] private GameObject[] optionsMusicObjects;
    private Image[] optionsSoundImages;
    private Image[] optionsMusicImages;
    [SerializeField] private Image VsyncImageSwitch;
    [SerializeField] private Sprite volumeSpriteOn;
    [SerializeField] private Sprite volumeSpriteOff;
    [SerializeField] private Sprite switchSpriteOn;
    [SerializeField] private Sprite switchSpriteOff;
    [SerializeField] private Animator optionsAnimator;


    [Header("Credits Block")]
    [SerializeField] private GameObject creditsFirstScreen;
    [SerializeField] private GameObject creditsSecondScreen;
    [SerializeField] private GameObject creditsArrow;

    int selectedInnerIndex = 0;
    bool selectedFile = false;
    int selectedFileNum = 0;






    [Header("Other")]
    [SerializeField] private Animator menuAnimator;

    private int selectedIndex;

    private bool side = false;

    private bool runCustomUpdate = false;

    private bool NewGameUpdate = false;
    private bool LoadGameUpdate = false;
    private bool OptionsUpdate = false;
    private bool CreditsUpdate = false;


    // Start is called before the first frame update
    void Start()
    {
        saveFileAnimator = saveFilePart.GetComponent<Animator>();
        yesNoAnimator = yesNoPart.GetComponent<Animator>();

        saveFileImages = new Image[saveFilesObjects.Length];

        optionsSoundImages = new Image[optionsSFXObjects.Length];
        optionsMusicImages = new Image[optionsMusicObjects.Length];



        int index = 0;
        foreach (GameObject obj in saveFilesObjects)
        {
            saveFileImages[index] = obj.GetComponent<Image>();
            if (File.Exists(Path.Combine(Application.persistentDataPath, GameManager.instance.Saves[index])))
            {
                saveFileImages[index].sprite = coloredSaveSprites[index];
                //TimePlayedTexts[index].text = "08:51:12";
            }
            else
            {
                TimePlayedTexts[index].text = "";
            }

            index++;
        }

        //SFX Init
        index = 0;
        foreach (GameObject obj in optionsSFXObjects)
        {
            optionsSoundImages[index] = obj.GetComponent<Image>();
            index++;
        }

        foreach (Image soundimage in optionsSoundImages)
        {
            soundimage.sprite = volumeSpriteOff;
        }
        for (int i = 0; i < GameManager.instance.VolumeSFX; i++)
        {
            optionsSoundImages[i].sprite = volumeSpriteOn;
        }

        //Music Init
        index = 0;
        foreach (GameObject obj in optionsMusicObjects)
        {
            optionsMusicImages[index] = obj.GetComponent<Image>();
            index++;
        }

        foreach (Image musicimage in optionsMusicImages)
        {
            musicimage.sprite = volumeSpriteOff;
        }
        for (int i = 0; i < GameManager.instance.VolumeMusic; i++)
        {
            optionsMusicImages[i].sprite = volumeSpriteOn;
        }
        //VSync init
        if (QualitySettings.vSyncCount == 0) VsyncImageSwitch.sprite = switchSpriteOff;
        else if (QualitySettings.vSyncCount == 1) VsyncImageSwitch.sprite = switchSpriteOn;

        leftButtons = new Button[leftButtonObjects.Length];
        rightButtons = new Button[rightButtonObjects.Length];

        index = 0;
        foreach (GameObject obj in rightButtonObjects)
        {
            rightButtons[index] = obj.GetComponent<Button>();
            index++;
        }
        index = 0;
        foreach (GameObject obj in leftButtonObjects)
        {
            leftButtons[index] = obj.GetComponent<Button>();
            index++;
        }

        creditsSecondScreen.SetActive(false);
        creditsArrow.SetActive(false);



        selectedIndex = 0;
        leftAnimator.Play(selectedIndex.ToString());
    }

    void Update()
    {
        if (!GameManager.instance.loading)
        {
            if (runCustomUpdate)
            {
                if (NewGameUpdate) NewGameLoop();
                else if (LoadGameUpdate) LoadGameLoop();
                else if (OptionsUpdate) OptionsLoop();
                else if (CreditsUpdate) CreditsLoop();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    if (!side) // pokud jsme na levé straně
                    {
                        switch (selectedIndex)
                        {
                            case 0://zmáčnuto tlačítko Start
                                ChangeSide();
                                AudioManager.Instance.PlaySound("buttonPressSound");
                                break;
                            case 1: //zmáčnuto Quit
                                    // ukaž are you sure, po vybrání exit
                                Exit(); break;
                            default: return;
                        }
                        return;
                    }
                    else // pravá strana
                    {
                        AudioManager.Instance.PlaySound("buttonPressSound");
                        switch (selectedIndex)
                        {
                            case 0: NewGame(); return;
                            case 1: LoadGame(); break;
                            case 2: break;
                            case 3: Options(); break;
                            case 4: Credits(); break;
                            case 5: Exit(); break;
                            default: return;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    HandleButtonMove(KeyCode.UpArrow);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    HandleButtonMove(KeyCode.DownArrow);
                    return;
                }
            }
        }
    }

    void Credits()
    {
        rightAnimator.speed = 0f;

        runCustomUpdate = true;
        CreditsUpdate = true;
        creditsArrow.SetActive(true);
        creditsFirstScreen.SetActive(true);
    }

    void CreditsLoop()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (creditsFirstScreen.activeSelf)
            {
                creditsFirstScreen.SetActive(false);
                creditsSecondScreen.SetActive(true);
            }
            else if (creditsSecondScreen.activeSelf)
            {
                creditsSecondScreen.SetActive(false);
                creditsFirstScreen.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rightAnimator.speed = 1f;

            runCustomUpdate = false;
            CreditsUpdate = false;
            creditsFirstScreen.SetActive(true);
            creditsArrow.SetActive(false);
            creditsSecondScreen.SetActive(false);
        }
    }

    void LoadGameLoop()
    {
        if (!selectedFile) //4 obrázky s výběrem save filu
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                rightAnimator.enabled = false;
                rightAnimator.enabled = true;
                saveFilePart.SetActive(false);
                saveFilePart.SetActive(true);
                rightAnimator.speed = 1f;
                runCustomUpdate = false;
                LoadGameUpdate = false;
                selectedIndex = 1;
                rightAnimator.Play(selectedIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                if (selectedInnerIndex == 4)
                {
                    rightAnimator.enabled = false;
                    rightAnimator.enabled = true;
                    saveFilePart.SetActive(false);
                    saveFilePart.SetActive(true);
                    rightAnimator.speed = 1f;
                    runCustomUpdate = false;
                    LoadGameUpdate = false;
                    selectedIndex = 0;
                    rightAnimator.Play(selectedIndex.ToString());
                    return;
                }
                yesNoPart.SetActive(true);
                if (File.Exists(Path.Combine(Application.persistentDataPath, GameManager.instance.Saves[selectedInnerIndex])))
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Načíst hru?";
                }
                else
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Začít novou hru?";
                }
                saveFilePart.SetActive(false);
                selectedFile = true;
                selectedFileNum = selectedInnerIndex + 1;
                selectedInnerIndex = 0;
                yesNoAnimator.Play(selectedInnerIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 1)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex += 2;
                }
                else if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    leftFrom = selectedInnerIndex;
                    selectedInnerIndex = 4;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex -= 2;
                }
                else if (selectedInnerIndex == 4)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex = leftFrom;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 2)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex++;
                }
                else if (selectedInnerIndex == 4)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    leftFrom = 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1 || selectedInnerIndex == 3)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex--;
                }
                else if (selectedInnerIndex == 4)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    leftFrom = 2;
                }
            }
            saveFileAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                switch (selectedInnerIndex)
                {
                    case 0:
                        Debug.Log("Loading game on file " + selectedFileNum.ToString());
                        DataPersistenceManager.Instance.LoadGame($"data{selectedFileNum}.game");
                        //DataPersistenceManager.Instance.LoadGame($"data{selectedFileNum}.game");
                        GameManager.instance.LoadScene(GameManager.instance.loadedScene);
                        break;
                    case 1:
                        Debug.Log("no in confirmation");
                        saveFilePart.SetActive(true);
                        yesNoPart.SetActive(false);
                        selectedFile = false;
                        selectedInnerIndex = selectedFileNum - 1;
                        saveFileAnimator.Play(selectedInnerIndex.ToString());
                        selectedFileNum = 0;
                        return;
                    default: break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                saveFilePart.SetActive(true);
                yesNoPart.SetActive(false);
                selectedFile = false;
                selectedInnerIndex = selectedFileNum - 1;
                saveFileAnimator.Play(selectedInnerIndex.ToString());
                selectedFileNum = 0;
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex--;
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedInnerIndex++;
                }

            }
            yesNoAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
    }
    void LoadGame()
    {
        rightAnimator.speed = 0f;

        selectedInnerIndex = 0;
        selectedFile = false;
        selectedFileNum = 0;
        saveFileAnimator.Play(selectedInnerIndex.ToString());
        runCustomUpdate = true;
        LoadGameUpdate = true;
    }


    void NewGameLoop()
    {
        if (!selectedFile) //4 obrázky s výběrem save filu
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                rightAnimator.enabled = false;
                rightAnimator.enabled = true;
                saveFilePart.SetActive(false);
                saveFilePart.SetActive(true);
                rightAnimator.speed = 1f;
                runCustomUpdate = false;
                NewGameUpdate = false;
                selectedIndex = 0;
                rightAnimator.Play(selectedIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                if (selectedInnerIndex == 4)
                {
                    rightAnimator.enabled = false;
                    rightAnimator.enabled = true;
                    saveFilePart.SetActive(false);
                    saveFilePart.SetActive(true);
                    rightAnimator.speed = 1f;
                    runCustomUpdate = false;
                    NewGameUpdate = false;
                    selectedIndex = 0;
                    rightAnimator.Play(selectedIndex.ToString());
                    return;
                }
                yesNoPart.SetActive(true);
                if (File.Exists(Path.Combine(Application.persistentDataPath, GameManager.instance.Saves[selectedInnerIndex])))
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Přepsat stávající hru?";
                }
                else
                {
                    newGameTextObject.GetComponent<TextMeshProUGUI>().text = "Začít novou hru?";
                }
                saveFilePart.SetActive(false);
                selectedFile = true;
                selectedFileNum = selectedInnerIndex + 1;
                selectedInnerIndex = 0;
                yesNoAnimator.Play(selectedInnerIndex.ToString());
                return;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 1)
                {
                    selectedInnerIndex += 2;
                }
                else if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    leftFrom = selectedInnerIndex;
                    selectedInnerIndex = 4;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (selectedInnerIndex == 2 || selectedInnerIndex == 3)
                {
                    selectedInnerIndex -= 2;
                }
                else if (selectedInnerIndex == 4)
                {
                    selectedInnerIndex = leftFrom;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0 || selectedInnerIndex == 2)
                {
                    selectedInnerIndex++;
                }
                else if (selectedInnerIndex == 4)
                {
                    leftFrom = 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1 || selectedInnerIndex == 3)
                {
                    selectedInnerIndex--;
                }
                else if (selectedInnerIndex == 4)
                {
                    leftFrom = 2;
                }
            }
            saveFileAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                AudioManager.Instance.PlaySound("buttonPressSound");
                switch (selectedInnerIndex)
                {
                    case 0:
                        Debug.Log("Starting game on file " + selectedFileNum.ToString());
                        DataPersistenceManager.Instance.NewGame($"data{selectedFileNum}.game");
                        //DataPersistenceManager.Instance.LoadGame($"data{selectedFileNum}.game");
                        GameManager.instance.LoadScene("Tavern");
                        break;
                    case 1:
                        Debug.Log("no in confirmation");
                        saveFilePart.SetActive(true);
                        yesNoPart.SetActive(false);
                        selectedFile = false;
                        selectedInnerIndex = selectedFileNum - 1;
                        saveFileAnimator.Play(selectedInnerIndex.ToString());
                        selectedFileNum = 0;
                        return;
                    default: break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                AudioManager.Instance.PlaySound("buttonPressSound");
                saveFilePart.SetActive(true);
                yesNoPart.SetActive(false);
                selectedFile = false;
                selectedInnerIndex = selectedFileNum - 1;
                saveFileAnimator.Play(selectedInnerIndex.ToString());
                selectedFileNum = 0;
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (selectedInnerIndex == 1) selectedInnerIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedInnerIndex == 0) selectedInnerIndex++;
            }
            yesNoAnimator.Play(selectedInnerIndex.ToString());
            return;
        }
    }
    void NewGame()
    {
        rightAnimator.speed = 0f;

        selectedInnerIndex = 0;
        selectedFile = false;
        selectedFileNum = 0;
        saveFileAnimator.Play(selectedInnerIndex.ToString());
        runCustomUpdate = true;
        NewGameUpdate = true;
    }

    void Options()
    {
        rightAnimator.speed = 0f;

        selectedInnerIndex = 0;
        optionsAnimator.Play(selectedInnerIndex.ToString());

        runCustomUpdate = true;
        OptionsUpdate = true;
    }

    void OptionsLoop()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (selectedInnerIndex != 3) selectedInnerIndex++;
            optionsAnimator.Play(selectedInnerIndex.ToString());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (selectedInnerIndex != 0) selectedInnerIndex--;
            optionsAnimator.Play(selectedInnerIndex.ToString());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (selectedInnerIndex == 0)
            {
                if (GameManager.instance.VolumeSFX != 0)
                {
                    GameManager.instance.VolumeSFX -= 1;
                    PlayerPrefs.SetInt("SFXVol", GameManager.instance.VolumeSFX);
                }

                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.VolumeSFX; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
            else if (selectedInnerIndex == 1)
            {
                if (GameManager.instance.VolumeMusic != 0)
                {
                    GameManager.instance.VolumeMusic -= 1;
                    PlayerPrefs.SetInt("MusicVol", GameManager.instance.VolumeMusic);
                }
                foreach (Image soundimage in optionsMusicImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.VolumeMusic; i++)
                {
                    optionsMusicImages[i].sprite = volumeSpriteOn;
                }
            }
            Debug.Log("SFX: " + GameManager.instance.VolumeSFX + ", " + (float)GameManager.instance.VolumeSFX / 10);
            Debug.Log("Music: " + GameManager.instance.VolumeMusic + ", " + (float)GameManager.instance.VolumeMusic / 10);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            if (selectedInnerIndex == 0)
            {
                if (GameManager.instance.VolumeSFX != 10)
                {
                    GameManager.instance.VolumeSFX += 1;
                    PlayerPrefs.SetInt("SFXVol", GameManager.instance.VolumeSFX);
                }

                foreach (Image soundimage in optionsSoundImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.VolumeSFX; i++)
                {
                    optionsSoundImages[i].sprite = volumeSpriteOn;
                }
            }
            else if (selectedInnerIndex == 1)
            {
                if (GameManager.instance.VolumeMusic != 10)
                {
                    GameManager.instance.VolumeMusic += 1;
                    PlayerPrefs.SetInt("MusicVol", GameManager.instance.VolumeMusic);
                }
                foreach (Image soundimage in optionsMusicImages)
                {
                    soundimage.sprite = volumeSpriteOff;
                }
                for (int i = 0; i < GameManager.instance.VolumeMusic; i++)
                {
                    optionsMusicImages[i].sprite = volumeSpriteOn;
                }
            }
            Debug.Log("SFX: " + GameManager.instance.VolumeSFX + ", " + GameManager.instance.VolumeSFX / 10);
            Debug.Log("Music: " + GameManager.instance.VolumeMusic + ", " + GameManager.instance.VolumeMusic / 10);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {


            switch (selectedInnerIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    AudioManager.Instance.PlaySound("buttonPressSound");
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
                    break;
                case 3:
                    AudioManager.Instance.PlaySound("buttonPressSound");
                    rightAnimator.speed = 1f;
                    optionsAnimator.Play("default");
                    runCustomUpdate = false;
                    OptionsUpdate = false;
                    return;
                default: return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Instance.PlaySound("buttonPressSound");
            rightAnimator.speed = 1f;
            optionsAnimator.Play("default");
            runCustomUpdate = false;
            OptionsUpdate = false;
        }

    }

    void ChangeSide()
    {
        selectedIndex = 0;
        if (side) //vpravo
        {
            menuAnimator.Play("Menu_Left");
            side = false;
            leftAnimator.Play(selectedIndex.ToString());
        }
        else //vlevo
        {
            menuAnimator.Play("Menu_Right");
            yesNoPart.SetActive(false);
            side = true;
            rightAnimator.Play(selectedIndex.ToString());
        }
    }

    private void HandleButtonMove(KeyCode pressed)
    {

        if (selectedIndex == 0)
        {
            yesNoPart.SetActive(false);
        }
        if (pressed == KeyCode.DownArrow)
        {
            if (side)
            {
                if (selectedIndex != 5)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedIndex++;
                    rightAnimator.Play(selectedIndex.ToString());
                }
            }
        }
        if (pressed == KeyCode.UpArrow)
        {
            if (side)
            {
                if (selectedIndex != 0)
                {
                    AudioManager.Instance.PlaySound("buttonSelectSound");
                    selectedIndex--;
                    rightAnimator.Play(selectedIndex.ToString());
                }
            }

        }
    }
    public void Play()
    {
        GameManager.instance.LoadScene("Tavern");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}